/**************************************************************
 * 
 * 唯一标识：9afe7420-7783-4395-9ebb-6490240e2fb9
 * 命名空间：Sgr.UPMS.Infrastructure.Checkers
 * 创建时间：2023/8/24 19:33:20
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.EntityFrameworkCore;
using Sgr.EntityFrameworkCore;
using Sgr.UPMS.Domain.Organizations;
using Sgr.UPMS.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Infrastructure.Checkers
{
    public class UserChecker : IUserChecker
    {
        private readonly SgrDbContext _context;

        public UserChecker(SgrDbContext context)
        {
            _context = context;
        }

        public Task<bool> LoginNameIsUniqueAsync(string loginName, long id = 0, CancellationToken cancellationToken = default)
        {
            return _context.Set<User>().AnyAsync(f => f.LoginName == loginName && f.Id != id, cancellationToken);
        }
    }
}
