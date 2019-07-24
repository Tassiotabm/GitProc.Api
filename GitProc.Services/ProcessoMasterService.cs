using GitProc.Data;
using GitProc.Data.Repository.Abstractions;
using GitProc.Model.Data;
using GitProc.Services.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GitProc.Services
{
    public class ProcessoMasterService : IProcessoMasterService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMovimentoRepository _movimentoRepository;

        public ProcessoMasterService(IUnitOfWork uow, IMovimentoRepository movimentoRepository)
        {
            _uow = uow;
            _movimentoRepository = movimentoRepository;
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

        public async Task<Movimento> GetMovimento(Guid processMasterId)
        {
            return await _movimentoRepository.GetFirstMovimento(processMasterId);
        }
    }
}
