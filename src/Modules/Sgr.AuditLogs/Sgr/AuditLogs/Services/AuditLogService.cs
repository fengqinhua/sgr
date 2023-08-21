/**************************************************************
 * 
 * 唯一标识：7588e563-50fe-45b9-a44a-2643ee5ef2bd
 * 命名空间：Sgr.AuditLogs.Services
 * 创建时间：2023/8/9 16:09:20
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.AuditLogs.Model;
using Sgr.Domain.Entities.Auditing;
using Sgr.EntityFrameworkCore;
using Sgr.Generator;
using Sgr.Utilities;
using System.Threading.Tasks;

namespace Sgr.AuditLogs.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditLogService : IAuditLogService
    {
        private readonly SgrDbContext _context;
        private readonly IAuditedOperator _auditedOperator;
        private readonly INumberIdGenerator _numberIdGenerator;

        public AuditLogService(SgrDbContext context, IAuditedOperator auditedOperator, INumberIdGenerator numberIdGenerator)
        {
            _context = context;
            _auditedOperator = auditedOperator;
            _numberIdGenerator = numberIdGenerator;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns></returns>
        public async Task OperateLogAsync(UserHttpRequestAuditInfo requestInfo)
        {
            if (requestInfo == null)
                return;

            var logOperate = new LogOperate(requestInfo.OrgId)
            {
                Id = _numberIdGenerator.GenerateUniqueId(),
                LoginName = requestInfo.LoginName,
                UserName = requestInfo.UserName,
                IpAddress = requestInfo.IpAddress,
                Location = "",
                ClientBrowser = requestInfo.ClientBrowser,
                ClientOs = requestInfo.ClientOs,
                HttpMethod = requestInfo.HttpMethod,
                OperateWay = requestInfo.OperateWay,
                Remark = requestInfo.StatusMessage == null ? "" : StringHelper.SubStringMaxLength(requestInfo.StatusMessage!, 500),
                RequestDescription = requestInfo.Description,
                RequestDuration = requestInfo.Duration,
                RequestParam = StringHelper.SubStringMaxLength(requestInfo.Param, 4000),
                RequestTime = requestInfo.OperateTime,
                RequestUrl = StringHelper.SubStringMaxLength(requestInfo.Url, 500),
                Status = requestInfo.Status
            };

            _auditedOperator.UpdateCreationAudited(logOperate);

            await _context.Set<LogOperate>().AddAsync(logOperate);
            await _context.SaveEntitiesAsync();
        }
    }
}
