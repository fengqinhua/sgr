/**************************************************************
 * 
 * 唯一标识：4b53fac5-47d2-49ff-b546-024130503bd5
 * 命名空间：Sgr.UPMS.Application.Commands.Organizations
 * 创建时间：2023/8/23 14:51:17
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

namespace Sgr.UPMS.Application.Commands.Organizations
{
    /// <summary>
    /// 组织机构认证
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为组织机构管理人员，希望通过关联上级组织机构，已使用诸如：数据报送等平台更多高级功能
    /// </remarks>
    public class AssociatedParentOrganizationCommand
    {
        /// <summary>
        /// 组织机构标识
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 上级组织机构标识
        /// </summary>
        public long? ParentId { get; set; }
    }
}
