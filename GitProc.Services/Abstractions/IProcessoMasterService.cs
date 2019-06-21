using GitProc.Model.Data;
using System.Threading.Tasks;

namespace GitProc.Services.Abstractions
{
    public interface IProcessoMasterService
    {
        Task<ProcessoMaster> SaveProcessoMaster(ProcessoMaster processo);
    }
}
