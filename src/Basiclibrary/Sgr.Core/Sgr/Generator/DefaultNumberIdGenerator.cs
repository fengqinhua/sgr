/**************************************************************
 * 
 * 唯一标识：a12df854-1ffd-4114-8276-aa446340a55e
 * 命名空间：Sgr.Generator
 * 创建时间：2023/7/28 16:30:52
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/


using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Generator
{
    /// <summary>
    /// 默认的长整型唯一标识生成器
    /// </summary>
    public class DefaultNumberIdGenerator: INumberIdGenerator
    {
        private readonly Snowflake _snowflake;

        /// <summary>
        /// 默认的长整型唯一标识生成器
        /// </summary>
        /// <param name="option"></param>
        public DefaultNumberIdGenerator(IOptions<SnowflakeOption> option)
        {
            if (option.Value != null)
                _snowflake = new Snowflake(option.Value.MachineId, option.Value.DatacenterId);
            else
                _snowflake = new Snowflake();
        }

        /// <summary>
        /// 生成不重复的长整型唯一标识
        /// </summary>
        /// <returns></returns>
        public long GenerateUniqueId()
        {
            return _snowflake.GetId();
        }
    }
}
