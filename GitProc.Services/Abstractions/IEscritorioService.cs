using GitProc.Model.Data;
using System;
using System.Threading.Tasks;

namespace GitProc.Services
{
    public interface IEscritorioService
    {
        Task<Escritorio> CreateEscritorio(Escritorio escritorio);
        Task RemoveEscritorio(Guid escritorioId);
        Task EditEscritorio(string endereco, string name, string CNPJ, Guid escritorioId);
    }
}