/**************************************************************
 * 
 * 唯一标识：8e08bc6c-b624-47e2-958d-eb6b393c2577
 * 命名空间：Sgr.Admin.WebHost
 * 创建时间：2023/7/30 23:35:50
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using FastEndpoints;
using System.Threading.Tasks;
using System.Threading;

namespace Sgr.Admin.WebHost
{
    /// <summary>
    /// 
    /// </summary>
    public class TestEndpoint : EndpointWithoutRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("/api/test");
            AllowAnonymous();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public override async Task HandleAsync(CancellationToken ct)
        {
            await SendAsync($"this is test!");
        }
    }
}
