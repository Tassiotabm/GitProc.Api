using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitProc.Api.Extensions.Globalization
{
    public class EFStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly GlobalizationDbContext _db;

        public EFStringLocalizerFactory(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GlobalizationDbContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            _db = new GlobalizationDbContext(optionsBuilder.Options);
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new EFStringLocalizer(_db);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new EFStringLocalizer(_db);
        }
    }
}
