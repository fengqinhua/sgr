/**************************************************************
 * 
 * 唯一标识：051909ae-b4b4-4f39-aa02-3b04f2570024
 * 命名空间：MediatR.Behaviors
 * 创建时间：2023/8/8 11:47:59
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr;
using Sgr.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Behaviors
{
    /// <summary>
    /// 日志审计
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class AuditLogBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _stopwatch;
        private readonly ICurrentUser _currentUser;
        private readonly IAuditLogService _auditLogService;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="auditLogService"></param>
        public AuditLogBehavior(ICurrentUser currentUser,
            IAuditLogService auditLogService)
        {
            _currentUser = currentUser;
            _auditLogService = auditLogService;

            _stopwatch = Stopwatch.StartNew();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="next"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _stopwatch.Restart();

            var response = await next();

            _stopwatch.Stop();

            

            return response;
        }
    }
}
