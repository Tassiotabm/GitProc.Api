using GitProc.Model.Data;
using System;
using System.Threading.Tasks;

namespace GitProc.Services.Abstractions
{
    public interface IProcessoMasterService
    {
        void UpdateProcessoMaster(ProcessoMaster processo);
        Task<ProcessoMaster> SaveProcessoMaster(ProcessoMaster processo);
        Task<Movimento> GetMovimento(Guid processMasterId);
    }
}
