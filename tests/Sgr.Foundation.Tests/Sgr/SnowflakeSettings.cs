using Microsoft.Extensions.Options;
using Sgr.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Foundation.Tests.Sgr
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
             DatacenterId =0,
              MachineId = 0
        }; 
    }
}
