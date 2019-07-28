using GitProc.Data.Repository.Model;
using GitProc.Model.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitProc.Services.Abstractions
{
    public interface IProcessoService
    {
        Task<IEnumerable<ProcessoMaster>> GetAllFromAdvogado(Guid userId);
        Task AddProcessMaster(Guid userId, string newProcesso, string nick);
        Task UpdateProcessAsync(Guid processoMasterId, string processNumber, string nick);
        Task SaveMovimento(List<Movimento> lista);
        Task AddProcesso(string comentario, string filePath, Processo processoData, string fileName);
        Task<IEnumerable<ProcessoMaster>> GetAllFromEscritorio(Guid userId);
        Task<IEnumerable<Comentario>> GetComentarios(Guid processId);
        Task<IEnumerable<Processo>> GetProcessos(Guid processoMasterId);
    }
}
