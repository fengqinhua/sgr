/**************************************************************
 * 
 * 唯一标识：e1f2bac5-05c7-4e53-8df9-cc0c76e406ae
 * 命名空间：Sgr.UPMS.Events
 * 创建时间：2023/8/27 20:13:07
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.UPMS.Events
{
    public class DepartmentNameChangedDomainEvent : INotification
    {
        public long DepartmentId { get; }
        public string DepartmentName { get; }

        public DepartmentNameChangedDomainEvent(long departmentId, string departmentName)
        {
            DepartmentId = departmentId;
            DepartmentName = departmentName;
        }
    }
}
