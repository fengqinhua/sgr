/**************************************************************
 * 
 * 唯一标识：7ad8355c-4e10-4d15-afd3-d2caaef8d0ff
 * 命名空间：Sgr.Foundation.API.OrgEndpoints
 * 创建时间：2023/7/30 22:53:33
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

//using FastEndpoints;
//using System.Threading.Tasks;
//using System.Threading;
//using Microsoft.AspNetCore.Builder;

//namespace Sgr.Foundation.API.OrgEndpoints
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    /// 
//    public class OrgGetByIdEndpoint : EndpointWithoutRequest
//    {
//        /// <summary>
//        /// 
//        /// </summary>
//        public override void Configure()
//        {
//            Verbs(Http.GET);
//            Routes("/api/org");
//            AllowAnonymous();

//            Description(b =>
//            {
//                //b.WithName("this is OrgGetByIdEndpoint");
//                b.WithDisplayName("this is OrgGetByIdEndpoint");
//            });
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="ct"></param>
//        /// <returns></returns>
//        //[AuditLogActionFilter("获取天气列表")]
//        public override async Task HandleAsync(CancellationToken ct)
//        {
//            await SendAsync($"this is OrgGetByIdEndpoint!", 200, ct);
//        }
//    }
//}
