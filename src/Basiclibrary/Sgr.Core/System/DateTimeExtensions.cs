/**************************************************************
 * 
 * 唯一标识：ae6be4be-c41d-4d36-946b-8a909f1f5fb9
 * 命名空间：System
 * 创建时间：2023/8/17 12:01:03
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr;
using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class DateTimeExtensions
    {
        public static DateTimeOffset ToDateTimeOffset(this DateTime dateTime)
        {
            return new DateTimeOffset(dateTime.ToUniversalTime());
        }

        //public static DateTimeOffset ToDateTimeOffset(this DateTime dateTime, int timeSpanHours = 8)
        //{
        //    return new DateTimeOffset(dateTime, TimeSpan.FromHours(timeSpanHours));
        //}
    }


}
