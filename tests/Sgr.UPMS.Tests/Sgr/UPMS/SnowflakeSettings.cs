using Microsoft.Extensions.Options;
using Sgr.Generator;

namespace Sgr.UPMS
{
    /// <summary>
    /// 
    /// </summary>
    public class SnowflakeSettings : IOptions<SnowflakeOption>
    {
        /// <summary>
        /// 
        /// </summary>
        public SnowflakeOption Value => new()
        {
            DatacenterId = 0,
            MachineId = 0
        };
    }
}
