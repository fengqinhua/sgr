/**************************************************************
 * 
 * 唯一标识：b7020366-c4fb-4afa-968d-cb4bebf4bcad
 * 命名空间：Sgr.Serialization
 * 创建时间：2023/8/17 11:51:41
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Diagnostics;

namespace Sgr.Serialization
{
    /// <summary>
    /// JSON中针对DateTime的序列化与反序列化
    /// </summary>
    public class DateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset>
    {

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            //Debug.Assert(typeToConvert == typeof(DateTimeOffset));

            if (reader.TokenType == JsonTokenType.String
                && DateTime.TryParse(reader.GetString(), out DateTime dateTime))
                return dateTime.ToDateTimeOffset();

            if (reader.TryGetDateTime(out DateTime value))
                return value.ToDateTimeOffset();

            return Constant.MinDateTimeOffset;
        }


        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.LocalDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
