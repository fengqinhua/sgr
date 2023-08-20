/**************************************************************
 * 
 * 唯一标识：6bbf462d-1b0a-4b0c-bf53-06476e502245
 * 命名空间：Sgr.UserAggregate
 * 创建时间：2023/8/6 14:43:17
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Domain.Entities.Auditing;
using Sgr.Domain.Entities;
using System;
using System.Collections.Generic;
using Sgr.Exceptions;
using Sgr.Utilities;
using System.Linq;

namespace Sgr.UPMS.Domain.Users
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : CreationAndModifyAuditedEntity<long, long>, IAggregateRoot, IOptimisticLock, IMustHaveOrg<long>
    {
        private List<UserDuty> _duties;
        private List<UserRole> _roles;

        /// <summary>
        /// 岗位列表
        /// </summary>
        public IEnumerable<UserDuty> Duties => _duties.AsReadOnly();
        /// <summary>
        /// 角色列表
        /// </summary>
        public IEnumerable<UserRole> Roles => _roles.AsReadOnly();

        /// <summary>
        /// 用户
        /// </summary>
        protected User()
        {
            _duties = new List<UserDuty>();
            _roles = new List<UserRole>();
        }

        /// <summary>
        /// 用户
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="orgId"></param>
        protected User(string loginName, long orgId) : this()
        {
            LoginName = loginName;
            ChangePassword("123456");
            State = EntityStates.Normal;
            OrgId = orgId;
        }

        /// <summary>
        /// 登录名称
        /// </summary>
        public string LoginName { get; private set; } = string.Empty;
        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPassword { get; private set; } = string.Empty;

        /// <summary>
        /// 首次登录时间
        /// </summary>
        public DateTimeOffset? FirstLoginTime { get; private set; }
        /// <summary>
        /// 最近一次登录时间
        /// </summary>
        public DateTimeOffset? LastLoginTime { get; private set; }
        /// <summary>
        /// 登录成功次数
        /// </summary>
        public int LoginSuccessCount { get; internal protected set; } = 0;
        /// <summary>
        /// 登录失败次数
        /// </summary>
        public int LoginFailCount { get; internal protected set; } = 0;

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string? UserName { get; set; } = string.Empty;
        /// <summary>
        /// 用户绑定的手机号码
        /// </summary>
        public string? UserPhone { get; set; }
        /// <summary>
        /// 用户绑定的邮箱地址
        /// </summary>
        public string? UserEmail { get; set; }
        /// <summary>
        /// 用户QQ号码
        /// </summary>
        public string? QQ { get; set; }
        /// <summary>
        /// 用户微信号码
        /// </summary>
        public string? Wechat { get; set; }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public bool IsSuperAdmin { get; private set; } = false;
        /// <summary>
        /// 账号状态
        /// </summary>
        public EntityStates State { get; internal protected set; } = EntityStates.Normal;

        /// <summary>
        /// 所属部门Id
        /// </summary>
        public long? DepartmentId { get; private set; }
        /// <summary>
        /// 所属部门名称
        /// </summary>
        public string DepartmentName { get; private set; } = "";

        /// <summary>
        /// 设置所属部门
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="departmentName"></param>
        public void BindDepartment(long? departmentId, string departmentName)
        {
            if (IsSuperAdmin)
                throw new BusinessException("超级管理员不可以绑定部门信息！");

            DepartmentId = departmentId;
            DepartmentName = departmentName;
        }

        /// <summary>
        /// 设置用户岗位列表(拥有多个岗位)
        /// </summary>
        /// <param name="dutyIds"></param>
        public void BindDuties(long[] dutyIds)
        {
            if (IsSuperAdmin)
                throw new BusinessException("超级管理员可以绑定岗位信息！");

            Check.NotNull(dutyIds, nameof(dutyIds));

            //移除不存在的
            _duties.RemoveAll(f => !dutyIds.Contains(f.DutyId));

            foreach (var duty in dutyIds)
            {
                if (_duties.Count(f => f.DutyId == duty) == 0)
                    _duties.Add(new UserDuty(duty));
            }
        }

        /// <summary>
        /// 设置用户角色列表(拥有多个角色)
        /// </summary>
        /// <param name="roleIds"></param>
        public void BindRoles(long[] roleIds)
        {
            if (IsSuperAdmin)
                throw new BusinessException("超级管理员可以绑定角色信息！");

            Check.NotNull(roleIds, nameof(roleIds));

            //移除不存在的
            _roles.RemoveAll(f => !roleIds.Contains(f.RoleId));

            foreach (var role in roleIds)
            {
                if (_roles.Count(f => f.RoleId == role) == 0)
                    _roles.Add(new UserRole(role));
            }
        }

        /// <summary>
        /// 登录成功
        /// </summary>
        /// <param name="status">是否成功</param>
        public void LoginComplete(bool status)
        {
            if (FirstLoginTime == null)
                FirstLoginTime = DateTimeOffset.UtcNow;
            LastLoginTime = DateTimeOffset.UtcNow;

            if (status)
                LoginSuccessCount += 1;
            else
                LoginFailCount += 1;
        }

        /// <summary>
        /// 校验密码是否正确
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckPassWord(string password)
        {
            return LoginPassword == HashHelper.CreateMd5(password);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="password"></param>
        /// <exception cref="BusinessException"></exception>
        public void ChangePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new BusinessException("密码不可以为空！");

            LoginPassword = HashHelper.CreateMd5(password);
        }

        /// <summary>
        /// 构造用于Hash的密码字符串
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        protected string buildPassword(string password)
        {
            if (IsSuperAdmin)
                return $"sgr superadmin:{password}";
            else
                return $"sgr:{password}";
        }

        #region IOptimisticLock (乐观锁)

        /// <summary>
        /// 行版本
        /// </summary>
        public long RowVersion { get; set; }

        #endregion

        #region IMustHaveOrg  (如果一个实体实现该接口，那么必须存储实体所在组织的ID.)

        /// <summary>
        /// 所属组织
        /// </summary>
        public long OrgId { get; internal protected set; }

        #endregion
    }
}
