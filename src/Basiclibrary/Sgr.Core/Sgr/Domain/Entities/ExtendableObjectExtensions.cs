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
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="extendableObject"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetString(this IExtendableObject extendableObject, string name)
        {
            var result = GetObject<string>(extendableObject, name);
            return result ?? "";
        }

        /// <summary>
        /// 设置字符串
        /// </summary>
        /// <param name="extendableObject"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetString(this IExtendableObject extendableObject, string name,string value)
        {
            SetObject(extendableObject, name, value);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="extendableObject"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool RemoveString(this IExtendableObject extendableObject, string name)
        {
            return RemoveString(extendableObject, name);
        }


        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="extendableObject"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T? GetObject<T>(this IExtendableObject extendableObject, string name) where T : class
        {
            Check.NotNull(extendableObject, nameof(extendableObject));
            Check.NotNull(name, nameof(name));

            string key = "c_" + name;

            if (!string.IsNullOrEmpty(extendableObject.ExtensionData))
            {
                var jsonNode = JsonNode.Parse(extendableObject.ExtensionData ?? "{}");
                var prop = jsonNode?[key];
                if (prop != null)
                {
                    return JsonSerializer.Deserialize<T>(prop); //return prop.GetValue<T>();
                }
                    
            }

            return null;
        }

        /// <summary>
        /// 设置对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="extendableObject"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetObject<T>(this IExtendableObject extendableObject, string name, T? value) where T : class
        {
            Check.NotNull(extendableObject, nameof(extendableObject));
            Check.NotNull(name, nameof(name));

            var jsonNode = JsonNode.Parse(extendableObject.ExtensionData ?? "{}");

            string key = "c_" + name;
            if (value == null)
                jsonNode!.AsObject().Remove(key);
            else
                jsonNode![key] = JsonSerializer.SerializeToNode(value);

            SetExtensionData(extendableObject, jsonNode);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="extendableObject"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool RemoveObject(this IExtendableObject extendableObject, string name)
        {
            Check.NotNull(extendableObject, nameof(extendableObject));
            Check.NotNull(name, nameof(name));

            if (extendableObject.ExtensionData == null)
                return false;

            string key = "c_" + name;
            var jsonNode = JsonNode.Parse(extendableObject.ExtensionData);
            jsonNode!.AsObject().Remove(key);

            SetExtensionData(extendableObject, jsonNode);

            return true;
        }

        /// <summary>
        /// 获取数据（值类型）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="extendableObject"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetValue<T>(this IExtendableObject extendableObject, string name) where T : struct 
        {
            Check.NotNull(extendableObject, nameof(extendableObject));
            Check.NotNull(name, nameof(name));

            if (string.IsNullOrEmpty(extendableObject.ExtensionData))
                return default;

            string key = "s_" + name;
            var jsonNode = JsonNode.Parse(extendableObject.ExtensionData ?? "{}");
            var prop = jsonNode?[key];

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

        /// <summary>
        /// 设置数据（值类型）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="extendableObject"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetValue<T>(this IExtendableObject extendableObject, string name, T value) where T : struct
        {
            Check.NotNull(extendableObject, nameof(extendableObject));
            Check.NotNull(name, nameof(name));

            string key = "s_" + name;

            var jsonNode = JsonNode.Parse(extendableObject.ExtensionData ?? "{}");

            if (EqualityComparer<T>.Default.Equals(value, default))
                jsonNode!.AsObject().Remove(key);
            else
                jsonNode![key] = JsonSerializer.SerializeToNode(value);

            SetExtensionData(extendableObject, jsonNode);
        }

        /// <summary>
        /// 删除数据（值类型）
        /// </summary>
        /// <param name="extendableObject"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool RemoveValue(this IExtendableObject extendableObject, string name)
        {
            Check.NotNull(extendableObject, nameof(extendableObject));
            Check.NotNull(name, nameof(name));

            if (extendableObject.ExtensionData == null)
                return false;

            string key = "s_" + name;
            var jsonNode = JsonNode.Parse(extendableObject.ExtensionData);
            jsonNode!.AsObject().Remove(key);

            SetExtensionData(extendableObject, jsonNode);

            return true;
        }

        private static void SetExtensionData(IExtendableObject extendableObject, JsonNode? jsonNode)
        {
            var data = jsonNode == null ? "{}" : jsonNode.ToJsonString();
            if (data == "{}")
                data = null;

            if (data != null && Encoding.UTF8.GetBytes(data).Length >= Constant.ExtendableObjectMaxLength)
                throw new BusinessException("ExtensionData exceeding the maximum number of bytes");

            extendableObject.ExtensionData = data;
        }
    }
}