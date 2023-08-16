/**************************************************************
 * 
 * 唯一标识：99798251-4570-4a14-8e7b-a89412aa48eb
 * 命名空间：Sgr.Serialization
 * 创建时间：2023/7/24 11:12:10
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
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            //Debug.Assert(typeToConvert == typeof(DateTime));
            
            if (reader.TokenType == JsonTokenType.String
                && DateTime.TryParse(reader.GetString(), out DateTime dateTime))
                return dateTime;

            if (reader.TryGetDateTime(out DateTime value))
                return value;

            return Constant.MinDateTimeOffset.DateTime;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
