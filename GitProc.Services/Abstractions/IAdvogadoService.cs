using GitProc.Model.Data;
using System;
using System.Threading.Tasks;

namespace GitProc.Services
{
    public interface IAdvogadoService
    {
        Task CreateAdvogado(Advogado advogado);
        Task RemoveAdvogado(Guid advogadoId);
        Task EditAdvogado(Advogado advogado, Guid adovogadoId);
        Task<Advogado> GetAdvogadoFromUserId(Guid userId);
    }
}