/**************************************************************
 * 
 * 唯一标识：5a31531c-d877-470d-8a3b-d4cf8fe9bb2e
 * 命名空间：MediatR.Behaviors
 * 创建时间：2023/8/18 8:59:51
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;
using FluentValidation;
using System.Linq;
using Sgr.Utilities;

namespace MediatR.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidatorBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
#if DEBUG
            string typeName = TypeHelper.GetGenericTypeName(request.GetType());
            _logger.LogInformation("Validating command {CommandType}", typeName);
#endif

            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
#if DEBUG
                _logger.LogWarning("Validation errors: {@ValidationErrors}",  failures);
#endif
                throw new FluentValidation.ValidationException($"Command Validation Errors for type {typeof(TRequest).Name}", failures);
            }

            return await next();
        }
    }
}
