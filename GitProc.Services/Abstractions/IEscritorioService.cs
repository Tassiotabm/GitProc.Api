using GitProc.Model.Data;
using System;
using System.Threading.Tasks;

namespace GitProc.Services
{
    public interface IEscritorioService
    {
        Task<Escritorio> CreateEscritorio(Escritorio escritorio, Guid advogadoId);
        Task RemoveEscritorio(Guid escritorioId);
        Task EditEscritorio(Escritorio escritorio);
    }
}