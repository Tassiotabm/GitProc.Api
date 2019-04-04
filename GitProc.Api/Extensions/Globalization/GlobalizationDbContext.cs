using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitProc.Api.Extensions.Globalization
{
    public class GlobalizationDbContext : DbContext
    {
        public DbSet<Culture> Cultures { get; set; }
        public DbSet<StringResource> StringResources { get; set; }

        public GlobalizationDbContext(DbContextOptions<GlobalizationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); ;
        }
    }

    public class Culture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<StringResource> Resources { get; set; }
    }

    public class StringResource
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public virtual Culture Culture { get; set; }
    }
}
