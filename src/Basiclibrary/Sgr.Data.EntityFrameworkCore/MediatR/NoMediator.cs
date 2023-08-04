/**************************************************************
 * 
 * 唯一标识：1665d107-5fe3-44a4-b500-f158807e5ec7
 * 命名空间：MediatR
 * 创建时间：2023/8/4 11:11:52
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR
{
    class NoMediator : IMediator
    {
        public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
#pragma warning disable CS8603 // 可能返回 null 引用。
            return default;
#pragma warning restore CS8603 // 可能返回 null 引用。
        }

        public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
        {
#pragma warning disable CS8603 // 可能返回 null 引用。
            return default;
#pragma warning restore CS8603 // 可能返回 null 引用。
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            return Task.CompletedTask;
        }

        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
#pragma warning disable CS8604 // 可能返回 null 引用。
            return Task.FromResult<TResponse>(default);
#pragma warning restore CS8604 // 可能返回 null 引用。
        }

#pragma warning disable CS8613 // 可能返回 null 引用。
        public Task<object> Send(object request, CancellationToken cancellationToken = default)
#pragma warning restore CS8613 // 可能返回 null 引用。
        {
#pragma warning disable CS8619 // 可能返回 null 引用。
            return Task.FromResult(default(object));
#pragma warning restore CS8619 // 可能返回 null 引用。
        }

        public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
        {
            return Task.CompletedTask;
        }
    }
}
