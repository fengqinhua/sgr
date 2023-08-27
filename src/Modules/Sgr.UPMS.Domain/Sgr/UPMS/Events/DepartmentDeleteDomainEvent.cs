/**************************************************************
 * 
 * 唯一标识：f29b0443-a72a-43f1-9b6b-e67e1b06afec
 * 命名空间：Sgr.UPMS.Events
 * 创建时间：2023/8/27 20:12:50
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
    public class DepartmentDeleteDomainEvent : INotification
    {
        public long DepartmentId { get; }

        public DepartmentDeleteDomainEvent(long departmentId)
        {
            this.DepartmentId = departmentId;
        }
    }
}
