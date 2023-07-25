/**************************************************************
 * 
 * 唯一标识：196e5c97-131e-4f84-baaf-6d231370075f
 * 命名空间：Sgr.Domain.Entities
 * 创建时间：2023/7/25 11:33:49
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using Sgr.Utilities;
using System.Runtime.InteropServices;

namespace Sgr.Domain.Entities
{
    /// <summary>
    /// IExtendableObject 的扩展方法
    /// </summary>
    public static class ExtendableObjectExtensions
    {
        public static T GetData<T>(this IExtendableObject extendableObject, string name)
        {
            Check.NotNull(extendableObject, nameof(extendableObject));
            Check.NotNull(name, nameof(name));

            if (string.IsNullOrEmpty(extendableObject.ExtensionData))
                return default;

            var jsonNode = JsonNode.Parse(extendableObject.ExtensionData);
            var prop = jsonNode[name];

            if (prop == null)
                return default;

            return prop.GetValue<T>();

            //if (typeof(T).IsPrimitiveExtendedIncludingNullable())
            //    return prop.GetValue<T>();
            //else
            //{
            //    return (T)prop.ToObject(typeof(T), jsonSerializer ?? JsonSerializer.CreateDefault());
            //}
        }

        public static void SetData<T>(this IExtendableObject extendableObject, string name, T value)
        {
            Check.NotNull(extendableObject, nameof(extendableObject));
            Check.NotNull(name, nameof(name));


            if (extendableObject.ExtensionData == null)
            {
                if (EqualityComparer<T>.Default.Equals(value, default))
                    return;
                extendableObject.ExtensionData = null;
            }
            else
            {
                var jsonNode = JsonNode.Parse(extendableObject.ExtensionData);
                if (value == null || EqualityComparer<T>.Default.Equals(value, default))
                    jsonNode.AsObject().Remove(name);
                else
                    jsonNode[name] = JsonSerializer.SerializeToNode(value);

                //else if (typeof(T).IsPrimitiveExtendedIncludingNullable())
                //{
                //    jsonNode[name] = "";
                //}

                var data = jsonNode.ToJsonString();
                if (data == "{}")
                    data = null;

                if (data != null && Encoding.UTF8.GetBytes(data).Length >= Constant.ExtendableObjectMaxLength)
                    throw new UserFriendlyException("ExtensionData exceeding the maximum number of bytes");

                extendableObject.ExtensionData = data;
            }
        }

        public static bool RemoveData(this IExtendableObject extendableObject, string name)
        {
            Check.NotNull(extendableObject, nameof(extendableObject));

            if (extendableObject.ExtensionData == null)
            {
                return false;
            }

            var jsonNode = JsonNode.Parse(extendableObject.ExtensionData);
            jsonNode.AsObject().Remove(name);

            var data = jsonNode.ToJsonString();
            if (data == "{}")
                data = null;
            extendableObject.ExtensionData = data;
            return true;
        }
    }
}