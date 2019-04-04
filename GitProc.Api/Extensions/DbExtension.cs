using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GitProc.Data;

namespace GitProc.Api.Extensions
{
    public static class DbExtension
    {
        public static void AddDbServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionStrings = configuration.GetSection("DefaultConnection").Value;
            services.AddDbContext<DomainDbContext>(options =>
                options.UseNpgsql(connectionStrings,
                    x => x.MigrationsAssembly("GitProc.Migrations")));
        }

        public static void UseMigration(this IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<DomainDbContext>().Database.Migrate();
            }

            Seed(app);
        }

        public static void Seed(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                 serviceScope.ServiceProvider.GetService<DomainDbContext>().EnsureSeedData();
            }
        }
    }
}
