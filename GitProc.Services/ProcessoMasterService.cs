using GitProc.Data;
using GitProc.Model.Data;
using GitProc.Services.Abstractions;
using System.Threading.Tasks;

namespace GitProc.Services
{
    public class ProcessoMasterService : IProcessoMasterService
    {
        private readonly IUnitOfWork _uow;

        public ProcessoMasterService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ProcessoMaster> SaveProcessoMaster(ProcessoMaster processo)
        {
            await _uow.ProcessoMaster.Add(processo);
            _uow.Complete();
            return processo;
        }

        public void UpdateProcessoMaster(ProcessoMaster processo)
        {        
            _uow.ProcessoMaster.Attach(processo);
            _uow.Complete();
        }
    }
}
