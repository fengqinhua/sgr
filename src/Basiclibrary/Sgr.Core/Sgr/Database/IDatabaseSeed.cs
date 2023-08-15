using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Database
{
    public interface IDatabaseSeed
    {
        Task SeedAsync();
    }
}
