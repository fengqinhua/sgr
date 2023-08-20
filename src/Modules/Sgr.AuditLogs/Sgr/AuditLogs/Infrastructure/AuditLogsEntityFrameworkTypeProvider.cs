/**************************************************************
 * 
 * 唯一标识：8fcc8373-4175-4226-a044-2f38bcd0032b
 * 命名空间：Sgr.AuditLogs.Infrastructure
 * 创建时间：2023/8/20 15:56:13
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.EntityFrameworkCore;
using Sgr.AuditLogs.Infrastructure.EntityConfigurations;
using Sgr.AuditLogs.Model;
using Sgr.EntityFrameworkCore;

namespace Sgr.AuditLogs.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditLogsEntityFrameworkTypeProvider : IEntityFrameworkTypeProvider
    {
        /// <summary>
        /// 注册Entity
        /// </summary>
        /// <param name="modelBuilder"></param>
        public void RegisterEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogOperate>();
        }

        /// <summary>
        /// 注册Configuration
        /// </summary>
        /// <param name="modelBuilder"></param>
        public void RegisterEntityConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LogOperateEntityTypeConfiguration());
        }
    }
}
