/**************************************************************
 * 
 * 唯一标识：7588e563-50fe-45b9-a44a-2643ee5ef2bd
 * 命名空间：Sgr.Foundation.API.Services
 * 创建时间：2023/8/9 16:09:20
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Services;
using System.Threading.Tasks;

namespace Sgr.Foundation.API.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditLogService : IAuditLogService
    {
        //private readonly
        //public AuditLogService() { }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task OperateLogAsync(UserHttpRequestAuditInfo requestInfo, bool status, string? message)
        {
            return Task.CompletedTask;
        }
    }
}
