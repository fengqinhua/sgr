/**************************************************************
 * 
 * 唯一标识：ec95aba9-88df-40c4-a0eb-aee591a45702
 * 命名空间：Sgr.OrganizationAggregate
 * 创建时间：2023/8/3 10:05:29
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/


using Sgr.Domain.Managers;
using Sgr.UPMS.Domain.Users;
using System.Threading.Tasks;

namespace Sgr.UPMS.Domain.Organizations
{
    /// <summary>
    /// 领域服务：组织机构管理
    /// </summary>
    public interface IOrganizationManage : ITreeNodeManage<Organization, long>
    { 



        /// <summary>
        /// 创建一个新的组织机构
        /// </summary>
        /// <param name="name"></param>
        /// <param name="orgTypeCode"></param>
        /// <param name="areaCode"></param>
        /// <param name="staffSizeCode"></param>
        /// <param name="leader"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <param name="orderNumber"></param>
        /// <param name="remarks"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        Task<Organization> CreateNewAsync(string name,
            string orgTypeCode,
            string areaCode,
            string staffSizeCode,
            string? leader,
            string? phone,
            string? email,
            string? address,
            int orderNumber,
            string? remarks,
            long? parentId);

        /// <summary>
        /// 组织注销前审查
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> CancellationExamination(Organization organization, User user);



    }
}
