using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Sgr.Domain.Entities.Auditing;
using Sgr.Domain.Repositories;
using Sgr.EntityFrameworkCore;
using Sgr.Generator;
using Sgr.RoleAggregate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Foundation.Tests.Sgr.Domain.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class RoleRepositoryTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<ICurrentUser> _currentUser;
        private readonly DbContextOptions<SgrDbContext> _dbOptions;

        /// <summary>
        /// 
        /// </summary>
        public RoleRepositoryTests()
        {
            //ef配置
            var serverVersion = new MySqlServerVersion(new Version(5, 7, 10));
            _dbOptions = new DbContextOptionsBuilder<SgrDbContext>()
                .UseMySql("server=localhost;port=3306;database=sgr;uid=root;pwd=1qaz@WSX;", serverVersion)
                .EnableSensitiveDataLogging()
                .LogTo((message) =>
                {
                    if (!message.Contains("CommandExecuting"))
                        return;
                    Console.WriteLine(message);
                })
                .Options;

            //ef entity config
            EntityFrameworkTypeRegistrar.Instance.Register<FoundationEntityFrameworkTypeProvider>();

            //IMediator
            _mediator = new Mock<IMediator>();
            _currentUser = new Mock<ICurrentUser>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected IRoleRepository CreateRepository()
        {
            var sgrDbContext = new SgrDbContext(_dbOptions, _mediator.Object);
            sgrDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            //sgrDbContext.ChangeTracker.AutoDetectChangesEnabled = false;
     
            var auditedOperator = new DefaultAuditedOperator(_currentUser.Object);
            var numberIdGenerator = new DefaultNumberIdGenerator(new SnowflakeSettings());
            return new RoleRepository(sgrDbContext, auditedOperator, numberIdGenerator);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public async Task CURD_Role_Success()
        {
            int orderNumber = 99;

            var repository = CreateRepository();

            //执行创建
            var newRole = await addNewAsync(repository);

            //执行修改
            await updateAsync(repository, newRole.Id, orderNumber);

            //执行查询
            var readRole = await repository.GetAsync(newRole.Id);

            Assert.IsNotNull(readRole);
            Assert.AreEqual(orderNumber, readRole.OrderNumber);

            Assert.AreEqual(newRole.Id, readRole.Id);
            Assert.AreEqual(newRole.RoleName, readRole.RoleName);
            Assert.AreEqual(newRole.State, readRole.State);
            Assert.AreEqual(newRole.Code, readRole.Code);
            Assert.AreEqual(readRole.RowVersion, 1);

            await updateAsync(repository, newRole.Id, orderNumber);
            Assert.AreEqual(readRole.RowVersion, 2);

            //执行删除
            await deleteAsync(repository, newRole.Id);
            var deleteRole = await repository.GetAsync(newRole.Id);
            Assert.IsNull(deleteRole);
        }

        /// <summary>
        /// 显式加载
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Query_Eager_Role_Success()
        {
            int id = 101;

            await ensureCreateAsync(id);

            var repository = CreateRepository();
            var role = await repository.GetAsync(id);
            Assert.IsNotNull(role);

            await repository.CollectionAsync(role, f => f.Resources);
            Assert.IsTrue(role.Resources.Count() > 0);
        }

        /// <summary>
        /// 预先加载
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Query_Explicit_Role_Success()
        {
            int id = 102;

            await ensureCreateAsync(id);

            var repository = CreateRepository();
            var role = await repository.GetAsync(id, new string[] { "Resources" });

            Assert.IsNotNull(role);
            Assert.IsTrue(role.Resources.Count() > 0);
        }



        private async Task ensureCreateAsync(long id)
        {
            var repository = CreateRepository();

            var role = await repository.GetAsync(id);
            if(role == null)
            {
                role = new RoleBuilder("SuperAdmin", "超级管理员", Constant.DEFAULT_ORGID, 0, "")
                    .AddOneResource(ResourceType.DataPermission, "管理所有数据")
                    .AddOneResource(ResourceType.FunctionalPermission, "组织权限管理")
                    .Build();
                role.Id = id;

                await repository.InsertAsync(role);
                await repository.UnitOfWork.SaveEntitiesAsync();
            }
        }

        private static async Task<Role> addNewAsync(IRoleRepository repository)
        {
            var role = new RoleBuilder("Admin", "系统管理员", Constant.DEFAULT_ORGID, 0, "单元测试")
                .AddOneResource(ResourceType.DataPermission, "管理仅当前组织数据")
                .AddOneResource(ResourceType.FunctionalPermission, "组织权限管理")
                .Build();
            await repository.InsertAsync(role);
            await repository.UnitOfWork.SaveEntitiesAsync();
            return role;
        }

        private static async Task updateAsync(IRoleRepository repository, long id, int orderNumber)
        {
            var updateRole = await repository.GetAsync(id, new string[] { "Resources" });
            //var updateRole = await repository.GetAsync(id);

            Assert.IsNotNull(updateRole);

            //await repository.CollectionAsync(updateRole, f => f.Resources);


            updateRole.OrderNumber = orderNumber;
            updateRole.AddPermission(ResourceType.DataPermission, "UPDATE TEST");
            await repository.UpdateAsync(updateRole);
            await repository.UnitOfWork.SaveEntitiesAsync();
        }

        private static async Task deleteAsync(IRoleRepository repository, long id)
        {
            await repository.DeleteAsync(id);
            await repository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
