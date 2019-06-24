using GitProc.Model.Data;
using System.Threading.Tasks;

namespace GitProc.Services.Abstractions
{
    public interface IProcessoMasterService
    {
        void UpdateProcessoMaster(ProcessoMaster processo);
        Task<ProcessoMaster> SaveProcessoMaster(ProcessoMaster processo);
    }
}
