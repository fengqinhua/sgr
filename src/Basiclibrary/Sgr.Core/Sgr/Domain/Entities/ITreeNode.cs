/**************************************************************
 * 
 * 唯一标识：78c99794-a0b3-4105-b89c-28e68e105319
 * 命名空间：Sgr.Domain.Entities
 * 创建时间：2023/8/3 11:53:44
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Domain.Entities
{
    /// <summary>
    /// 树形结构
    /// </summary>
    public interface ITreeNode<TPrimaryKey>
    {
        /// <summary>
        /// 上级节点Id
        /// </summary>
        TPrimaryKey ParentId { get; }
        /// <summary>
        /// 树节点层次目录
        /// </summary>
        string NodePath { get;}
    }
}
