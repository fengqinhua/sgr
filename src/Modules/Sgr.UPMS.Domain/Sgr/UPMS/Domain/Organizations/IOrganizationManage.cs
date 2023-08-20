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
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="orgTypeCode"></param>
        /// <param name="areaCode"></param>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        Task<Organization> CreateNewAsync(
            string code,
            string name,
            string orgTypeCode,
            string areaCode,
            long ParentId = 0);
    }
}
