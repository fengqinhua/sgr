/**************************************************************
 * 
 * 唯一标识：6cfb5e8c-9936-4314-a864-ca3a9af52ce5
 * 命名空间：Sgr.AspNetCore
 * 创建时间：2023/8/18 21:19:58
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Net;

namespace Sgr.AspNetCore.ExceptionHandling
{
    public interface IExceptionToErrorInfo
    {
        Tuple<HttpStatusCode, ServiceErrorInfo> Convert(Exception exception);
    }
}
