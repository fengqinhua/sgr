using Sgr.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Database
{
    public class FoundationDataBaseInitialize: IDataBaseInitialize
    {
        private readonly SgrDbContext _sgrDbContext;

        public FoundationDataBaseInitialize(SgrDbContext sgrDbContext)
        {
            _sgrDbContext = sgrDbContext;
        }

        public int Order => 100;

        public Task Initialize()
        {
            throw new NotImplementedException();
        }


    }
}
