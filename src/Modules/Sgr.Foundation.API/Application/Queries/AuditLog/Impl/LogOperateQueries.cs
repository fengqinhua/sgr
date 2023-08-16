/**************************************************************
 * 
 * 唯一标识：f55155ec-24f9-41a1-95c8-e5e3db127af6
 * 命名空间：Sgr.Foundation.API.Application.Queries.AuditLog.Impl
 * 创建时间：2023/8/16 17:34:13
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.EntityFrameworkCore;
using Sgr.Application.ViewModels;
using Sgr.AuditLogAggregate;
using Sgr.Domain.Entities;
using Sgr.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sgr.Foundation.API.Application.Queries.AuditLog.Impl
{
    public class LogOperateQueries : ILogOperateQueries
    {
        private readonly SgrDbContext _context;
        private readonly ICurrentUser _currentUser;

        public LogOperateQueries(SgrDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<OutLogOperate> GetAsync(long id)
        {
            var result = await _context.Set<LogOperate>()
                 .Where(f => f.Id == id)
                 .Select(f => new OutLogOperate()
                 {
                     ClientBrowser = f.ClientBrowser,
                     ClientOs = f.ClientOs,
                     HttpMethod = f.HttpMethod,
                     IpAddress = f.IpAddress,
                     Location = f.Location,
                     LoginName = f.LoginName,
                     LoginWay = f.LoginWay,
                     Remark = f.Remark,
                     RequestDescription = f.RequestDescription,
                     RequestDuration = f.RequestDuration,
                     RequestParam = f.RequestParam,
                     RequestTime = f.RequestTime,
                     RequestUrl = f.RequestUrl,
                     Status = f.Status,
                     UserName = f.UserName
                 })
                 .FirstOrDefaultAsync();

            if (result == null)
                throw new KeyNotFoundException($"LogOperate Key = {id} Not Found!");

            return result;
        }


 
        public async Task<PagedResponse<OutLogOperatePaged>> GetListAsync(InLogOperatePagedRequest request)
        {
            Check.NotNull(request, nameof(request));
            
            //如果无法获取组织标识则返回空数据
            if (!long.TryParse(_currentUser.OrgId, out long orgId))
                return new PagedResponse<OutLogOperatePaged>();

            var query = _context.Set<LogOperate>()
                .WithOrg(orgId);

            if (request.RequestUser != null)
                query = query.Where(f => EF.Functions.Like(f.UserName, $"%{request.RequestUser}%"));

            if (request.RequestDescription != null)
                query = query.Where(f => EF.Functions.Like(f.RequestDescription, $"%{request.RequestDescription}%"));

            if (request.RequestStart != null)
                query = query.Where(f => f.RequestTime >= request.RequestStart!);

            if (request.RequestEnd != null)
                query = query.Where(f => f.RequestTime <= request.RequestEnd!);

            if (request.Status != null)
                query = query.Where(f => f.Status == request.Status!);

            //Expression<Func<LogOperate, decimal>> orderByFunc = f => f.Id;
            //Expression<Func<Student, bool>> studentFunc = x =>
            //        (id == 0 || x.Id == id) &&
            //        (string.IsNullOrWhiteSpace(name) || x.Name.Contains(name)) &&
            //        (string.IsNullOrWhiteSpace(address) || x.Address.Contains(address)) &&
            //        (!status.HasValue || x.Status == status.Value) &&
            //        (createTime == null || x.CreateTime <= createTime);
            
            return await query.OrderBy(f=>f.Id)
                 .Select(f => new OutLogOperatePaged()
                 {
                     Id = f.Id,
                     ClientBrowser = f.ClientBrowser,
                     ClientOs = f.ClientOs,
                     IpAddress = f.IpAddress,
                     Location = f.Location,
                     LoginName = f.LoginName,
                     Remark = f.Remark,
                     RequestDescription = f.RequestDescription,
                     RequestDuration = f.RequestDuration,
                     RequestTime = f.RequestTime,
                     RequestUrl = f.RequestUrl,
                     Status = f.Status,
                 })
                 .ToPagedListByPageSizeAsync(request.PageIndex, request.PageSize);
        }
    }
}
