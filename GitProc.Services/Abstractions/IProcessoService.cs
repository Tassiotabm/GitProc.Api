using GitProc.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitProc.Services.Abstractions
{
    public interface IProcessoService
    {
        Task CreateProcessoAsync(Guid userId, string newProcesso);
        Task UpdateProcessAsync(Guid processoMasterId, string processNumber);
        Task<IEnumerable<Processo>> GetAllFromAdvogado(Guid AdvogadoId);
        Task<IEnumerable<Processo>> GetAllFromEscritorio(Guid userId);
    }
}
