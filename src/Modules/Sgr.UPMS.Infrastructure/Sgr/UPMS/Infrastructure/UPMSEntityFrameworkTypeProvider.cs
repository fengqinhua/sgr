/**************************************************************
 * 
 * 唯一标识：6a216f18-068f-4457-b436-a36ca66dfde9
 * 命名空间：Sgr.UPMS.Infrastructure
 * 创建时间：2023/8/20 17:43:52
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.EntityFrameworkCore;
using Sgr.EntityFrameworkCore;
using Sgr.UPMS.Domain.Departments;
using Sgr.UPMS.Domain.Duties;
using Sgr.UPMS.Domain.LogLogins;
using Sgr.UPMS.Domain.Organizations;
using Sgr.UPMS.Domain.Roles;
using Sgr.UPMS.Domain.Users;
using Sgr.UPMS.Domain.UserTokens;
using Sgr.UPMS.Infrastructure.EntityConfigurations;

namespace Sgr.UPMS.Infrastructure
{
    public class UPMSEntityFrameworkTypeProvider : IEntityFrameworkTypeProvider
    {
        public void RegisterEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogLogin>();

            modelBuilder.Entity<Organization>();
            modelBuilder.Entity<Department>();
            modelBuilder.Entity<Duty>();

            modelBuilder.Entity<Role>();
            modelBuilder.Entity<RoleResource>();

            modelBuilder.Entity<User>();
            modelBuilder.Entity<UserDuty>();
            modelBuilder.Entity<UserRole>();

            modelBuilder.Entity<UserRefreshToken>();
        }

        public void RegisterEntityConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LogLoginEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new OrganizationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DutyEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleResourceEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserDutyEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new UserRefreshTokenConfiguration());
        }
    }
}
