using GitProc.Data;
using GitProc.Data.Repository;
using GitProc.Data.Repository.Abstractions;
using GitProc.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitProc.Api.Extensions
{
    public static class DomainExtension
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAdvogadoRepository, AdvogadoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdvogadoService, AdvogadoService>();

        }
    }
}
