using GitProc.Model.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitProc.Services.Abstractions
{
    public interface IProcessoService
    {
        Task CreateProcessoAsync(Guid userId, string newProcesso);
        Task UpdateProcessAsync(Guid processoMasterId, string processNumber);
        Task<IEnumerable<Processo>> GetAllFromAdvogado(Guid AdvogadoId);
        Task<IEnumerable<Processo>> GetAllFromEscritorio(Guid userId);
        Task SaveMovimento(List<Movimento> lista);
        Task AddProcesso(string comentario, string filePath, Processo processoData);
    }
}
