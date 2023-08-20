/**************************************************************
 * 
 * 唯一标识：67b345ca-abd5-4bdc-9707-7810e2772a9c
 * 命名空间：MediatR.Behaviors
 * 创建时间：2023/8/18 9:31:51
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using Sgr.Utilities;

namespace Sgr.MediatR.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            string typeName = TypeHelper.GetGenericTypeName(request.GetType());

            _logger.LogInformation("Handling command {CommandName} ({@Command})", typeName, request);
            var response = await next();
            _logger.LogInformation("Command {CommandName} handled - response: {@Response}", typeName, response);

            return response;
        }
    }
}
