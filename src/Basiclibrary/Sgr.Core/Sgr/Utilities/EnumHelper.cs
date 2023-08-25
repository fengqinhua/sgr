/**************************************************************
 * 
 * 唯一标识：0723f66b-f1e9-461b-b724-ff4f9a25daf4
 * 命名空间：Sgr.Utilities
 * 创建时间：2023/7/24 9:31:24
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Sgr.Utilities
{
    /// <summary>
    /// 枚举工具类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 字符串转枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static TEnum StringToEnum<TEnum>(string value, bool ignoreCase = true) where TEnum : struct
        {
            Check.NotNull(value, nameof(value));
            return (TEnum) Enum.Parse(typeof(TEnum), value, ignoreCase);
        }

        public static IEnumerable<NameValue> EnumToNameValues<TEnum>() where TEnum : struct
        {
            Type type = typeof(TEnum);

            List<NameValue> result = new List<NameValue>();
            if(type.IsEnum)
            {
                string[] names = Enum.GetNames(type);
                Array values = Enum.GetValues(type);
                if(names.Length > 0
                    && names.Length == values.Length) {

                    for (int i = 0; i < names.Length; i++)
                    {
                        string name = names[i];
                        string value = $"{values.GetValue(i)}";

                        var field = type.GetField(name);
                        if(field != null)
                        {
                            var att = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute), false);
                            name = ((DescriptionAttribute)att).Description;
                        }
                        result.Add(new NameValue(name, value));
                    }
                }
   
            }
            return result;
        }
    }
}
