using System.Threading.Tasks;

namespace GitProc.Services
{
    public interface ITribunalService
    {
        Task GetOnlineProcessData(string processoNumber);
    }
}