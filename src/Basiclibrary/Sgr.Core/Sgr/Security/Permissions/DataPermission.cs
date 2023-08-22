/**************************************************************
 * 
 * 唯一标识：08831aa0-a57e-4686-8e61-3a945621406c
 * 命名空间：Sgr.Security.Permissions
 * 创建时间：2023/8/22 15:23:20
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Exceptions;
namespace Sgr.Security.Permissions
{
    public class DataPermission
    {
        public DataPermission(string name,
            string category,
            string description = "")
        {
            if (string.IsNullOrEmpty(name))
                throw new BusinessException("Data Permission Name Is Null Or Empty");

            if (string.IsNullOrEmpty(category))
                throw new BusinessException("Data Permission Category Is Null Or Empty");

            Name = name;
            Category = category;
            Description = string.IsNullOrEmpty(description) ? name : description;
        }

        /// <summary>
        /// 数据权限项名称
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 数据权限项描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 数据分类
        /// </summary>
        public string Category { get; set; } = string.Empty;
    }
}
