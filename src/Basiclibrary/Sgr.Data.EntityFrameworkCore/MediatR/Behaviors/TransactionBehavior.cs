/**************************************************************
 * 
 * 唯一标识：ab23e4f8-e429-40a4-bd9a-764c232c53ce
 * 命名空间：MediatR.Behaviors
 * 创建时间：2023/8/18 9:34:10
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sgr.Application.IntegrationEvents;
using Sgr.EntityFrameworkCore;
using Sgr.Exceptions;
using Sgr.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
        private readonly SgrDbContext _dbContext;
        private readonly IIntegrationEventService _integrationEventService;

        public TransactionBehavior(SgrDbContext dbContext,
            IIntegrationEventService integrationEventService,
            ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(SgrDbContext));
            _integrationEventService = integrationEventService ?? throw new ArgumentException(nameof(IIntegrationEventService));
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse? response = default;

#if DEBUG
            string typeName = TypeHelper.GetGenericTypeName(request.GetType());
#endif

            try
            {
                if (_dbContext.HasActiveTransaction)
                {
                    return await next();
                }

                var strategy = _dbContext.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;

                    await using var transaction = await _dbContext.BeginTransactionAsync();
                    using (_logger.BeginScope(new List<KeyValuePair<string, object>> { new("TransactionContext", transaction.TransactionId) }))
                    {
#if DEBUG
                        _logger.LogInformation("Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.TransactionId, typeName, request);
#endif
                        response = await next();
#if DEBUG
                        _logger.LogInformation("Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);
#endif

                        await _dbContext.CommitTransactionAsync(transaction);

                        transactionId = transaction.TransactionId;
                    }

                    await _integrationEventService.PublishEventsThroughEventBusAsync(transactionId);
                });


                return response == null ? throw new BusinessException("command response is null") : response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Handling transaction for {CommandName} ({@Command})", TypeHelper.GetGenericTypeName(request.GetType()), request);
                throw;
            }
        }
    }

}
