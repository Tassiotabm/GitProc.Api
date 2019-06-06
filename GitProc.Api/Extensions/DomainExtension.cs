using GitProc.Data;
using GitProc.Data.Repository;
using GitProc.Data.Repository.Abstractions;
using GitProc.Services;
using GitProc.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<IEscritorioService, EscritorioService>();
            services.AddScoped<IEscritorioRepository, EscritorioRepository>();
            services.AddScoped<IProcessoRepository, ProcessoRepository>();
            services.AddScoped<IProcessoService, ProcessoService>();
            services.AddScoped<ITribunalService, TribunalService>();
        }
    }
}
