/**************************************************************
 * 
 * 唯一标识：4dc15b23-3d1a-4390-9b60-8141360dce03
 * 命名空间：Sgr.UPMS.Domain.Departments
 * 创建时间：2023/8/27 19:41:54
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Domain.Managers;
using Sgr.UPMS.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.UPMS.Domain.Departments
{
    public interface IDepartmentManage : ITreeNodeManage<Department, long>
    {
        Task<Department> CreateNewAsync(string name,
            int orderNumber,
            string? remarks,
            string? leader,
            string? phone,
            string? email,
            long orgId,
            long? parentId);
    }
}
