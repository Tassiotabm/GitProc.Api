using GitProc.Model.Data;
using System.Threading.Tasks;

namespace GitProc.Services
{
    public interface ITribunalService
    {
        Task<ProcessoMaster> GetOnlineProcessData(string processoNumber);
        Task UpdateProcess(ProcessoMaster processo);
    }
}