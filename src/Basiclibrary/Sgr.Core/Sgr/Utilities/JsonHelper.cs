/**************************************************************
 * 
 * 唯一标识：5a6652d2-7a1b-4f7e-ac7a-2a3cf2187407
 * 命名空间：Sgr.Utilities
 * 创建时间：2023/7/24 10:14:08
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 *  https://learn.microsoft.com/zh-cn/dotnet/standard/serialization/system-text-json/how-to?pivots=dotnet-8-0
 *  https://www.cnblogs.com/cdaniu/p/15969988.html
 *  
 *  字符编码问题 【https://blog.csdn.net/zls365365/article/details/124162096】
 *     默认的 System.Text.Json 序列化的时候会把所有的非 ASCII 的字符进行转义，这就会导致很多时候我们的一些非 ASCII 的字符就会变成 \uxxxx 这样的形式，很多场景下并不太友好，我们可以配置字符编码来解决被转义的问题。   
 *     另外，对于一些包含 html 标签的文本即使指定了所有字符集也会被转义，这是出于安全考虑。如果觉得不需要转义也可以配置，配置使用 JavaScriptEncoder.UnsafeRelaxedJsonEscaping 即可
 *     
 *  对于枚举类型属性值（非数字型）的序列化，需要加 [JsonStringEnumConverter] 特性 【https://www.cnblogs.com/dudu/p/11562019.html】
 *  
 *  
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sgr.Utilities
{
    /// <summary>
    /// JSON工具类
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// 获取JSON序列化参数
        /// </summary>
        /// <returns></returns>
        public static JsonSerializerOptions GetJsonSerializerOptions()
        {
            var serializerOptions = new JsonSerializerOptions();
            UpdateJsonSerializerOptions(serializerOptions);

            return serializerOptions;
        }

        /// <summary>
        /// 设置JSON序列化参数
        /// </summary>
        /// <returns></returns>
        public static void UpdateJsonSerializerOptions(JsonSerializerOptions options)
        {
            Check.NotNull(options, "options");

            //// 设置编码格式
            //options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

#if DEBUG
            // 是否格式化文本
            options.WriteIndented = true;
#endif

            // 添加时间格式化转换器
            options.Converters.Add(new Serialization.DateTimeOffsetJsonConverter());

            // 字段采用驼峰式命名
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            // 忽略null值
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

            // 忽略只读字段
            options.IgnoreReadOnlyProperties = true;

            // 允许属性值末尾存在逗号
            options.AllowTrailingCommas = true;

            // 处理循环引用类型
            options.ReferenceHandler = ReferenceHandler.IgnoreCycles;

            //序列化/反序列化数字解析方式
            options.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals |
                      JsonNumberHandling.AllowReadingFromString |
                      JsonNumberHandling.WriteAsString;

        }

        /// <summary>
        /// 将实体类序列化为JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(T data)
        {
            var options = GetJsonSerializerOptions();
            return JsonSerializer.Serialize<T>(data, options);
        }

        /// <summary>
        /// 反序列化JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T? DeserializeObject<T>(string json) where T : class
        {
            Check.StringNotNullOrEmpty(json, nameof(json));

            var options = GetJsonSerializerOptions();
            return JsonSerializer.Deserialize<T>(json, options);
        }
    }
}
