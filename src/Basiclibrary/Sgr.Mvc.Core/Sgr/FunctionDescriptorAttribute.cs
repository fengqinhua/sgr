/**************************************************************
 * 
 * 唯一标识：427ec745-bb0b-42c0-b58d-b61a6fbf2c20
 * 命名空间：Sgr.Middlewares
 * 创建时间：2023/8/8 21:44:30
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;

namespace Sgr
{
    /// <summary>
    /// 函数描述
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class FunctionDescriptorAttribute : Attribute
    {
        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunctionName { get; private set; }

        /// <summary>
        /// 函数描述
        /// </summary>
        /// <param name="functionName"></param>
        public FunctionDescriptorAttribute(string functionName)
        {
            FunctionName = functionName;
        }
    }
}