using GitProc.Data.Repository.Abstractions;
using GitProc.Model.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitProc.Data.Repository
{
    public interface IProcessoRepository : IRepository<Processo>
    {
        Task<List<Processo>> GetAllAdvogadoInfos(Guid advogadoId);
        Task<List<Processo>> GetAllEscrotorioInfo(Guid escritorioId);
    }
}