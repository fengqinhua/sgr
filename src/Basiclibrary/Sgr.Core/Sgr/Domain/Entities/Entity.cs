/**************************************************************
 * 
 * 唯一标识：5f084701-e4b0-4abe-91a9-58746c95b847
 * 命名空间：Sgr.Domain.Entities
 * 创建时间：2023/7/25 20:32:45
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Sgr.Domain.Entities
{
    /// <summary>
    /// 实体类型基类（字符串）
    /// </summary>
    [Serializable]
    public abstract class EntityBase : IEntity<string>
    {
        /// <summary>
        /// ID ，实体的唯一标识符
        /// </summary>
        public string Id { get; set; } = "";

        /// <summary>
        ///  检查此实体是否是暂时的
        /// </summary>
        /// <returns></returns>
        public bool IsTransient()
        {
            return string.IsNullOrEmpty(Id);
        }

        /// <summary>
        /// 实体比较
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool EntityEquals(object obj)
        {
            if (obj == null || obj is not EntityBase)
                return false;

            //检查两个对象的引用地址是否一致，如果一致则认为是同一对象
            if (ReferenceEquals(this, obj))
                return true;

            //检查两个对象的类型是否一致，如果不一致则认为不是同一对象
            if (this.GetType() != obj.GetType())
                return false;

            var other = (EntityBase)obj;
            if (other.IsTransient() || this.IsTransient())
                return false;
            else
                return Id.Equals(other.Id);
        }

        /// <summary>
        /// 转字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[{GetType().Name} {Id}]";
        }

    }

    /// <summary>
    /// 实体类型基类（值类型）
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    [Serializable]
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey> where TPrimaryKey : struct
    {
        /// <summary>
        /// ID ，实体的唯一标识符
        /// </summary>
        public virtual TPrimaryKey Id { get; set; }

        /// <summary>
        /// 检查此实体是否是暂时的（检查Id是否为空）
        /// </summary>
        /// <returns></returns>
        public virtual bool IsTransient()
        {
            //如果唯一标识为缺省值，那么则认为该实体是临时对象，还未进行持久化
            if (EqualityComparer<TPrimaryKey>.Default.Equals(Id, default))
            {
                return true;
            }

            //如果唯一标识为int类型，那么Id必须大于0，否则认为该实体是临时对象，还未进行持久化
            if (typeof(TPrimaryKey) == typeof(int))
            {
                return Convert.ToInt32(Id) <= 0;
            }
            //如果唯一标识为long类型，那么Id必须大于0，否则认为该实体是临时对象，还未进行持久化
            if (typeof(TPrimaryKey) == typeof(long))
            {
                return Convert.ToInt64(Id) <= 0;
            }

            return false;
        }

        /// <summary>
        /// 实体比较
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool EntityEquals(object obj)
        {
            if (obj == null || obj is not Entity<TPrimaryKey>)
                return false;

            //检查两个对象的引用地址是否一致，如果一致则认为是同一对象
            if (ReferenceEquals(this, obj))
                return true;

            //检查两个对象的类型是否一致，如果不一致则认为不是同一对象
            if (this.GetType() != obj.GetType())
                return false;

            var other = (Entity<TPrimaryKey>)obj;
            if (other.IsTransient() || this.IsTransient())
                return false;
            else
                return Id.Equals(other.Id);
        }

        /// <summary>
        /// 转字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[{GetType().Name} {Id}]";
        }
    }
}
