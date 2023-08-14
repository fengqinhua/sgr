using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Sgr.EntityFrameworkCore;
using System;
using System.IO;

namespace Sgr.Admin.WebHost.Infrastructure.Factories
{
    public class SgrDbContextDesignFactory : IDesignTimeDbContextFactory<SgrDbContext>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public SgrDbContext CreateDbContext(string[] args)
        {
            //Debugger.Launch();

            var config = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
               .AddJsonFile("appsettings.json")
               .AddEnvironmentVariables()
               .Build();

            var serverVersion = new MySqlServerVersion(new Version(config.GetRequiredString("ConnectionStrings:SgrDBVersion")));

            var optionsBuilder = new DbContextOptionsBuilder<SgrDbContext>()
                .UseMySql(config.GetRequiredString("ConnectionStrings:SgrDB"), serverVersion, b => b.MigrationsAssembly("Sgr.Admin.WebHost"));

            var dbContext = new SgrDbContext(optionsBuilder.Options, new NoMediator());
            return dbContext;
        }
    }
}
