using GitProc.Data.Repository.Abstractions;
using GitProc.Data.Repository.Model;
using GitProc.Model.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitProc.Data.Repository
{
    public interface IProcessoRepository : IRepository<Processo>
    {
        Task<List<ProcessoMaster>> GetAllAdvogadoInfos(Guid advogadoId);
        Task<List<ProcessoData>> GetAllProcesso(Guid processMasterId);
        Task<List<ProcessoMaster>> GetAllEscrotorioInfo(Guid escritorioId);
    }
}