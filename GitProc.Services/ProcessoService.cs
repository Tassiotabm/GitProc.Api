using GitProc.Data;
using GitProc.Model.Data;
using GitProc.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitProc.Services
{
    public class ProcessoService : IProcessoService
    {
        private readonly IUnitOfWork _uow;
        private readonly IProcessoMasterService _processoMasterService;

        public ProcessoService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task CreateProcessoAsync(Processo newProcesso)
        {
            await _uow.Processo.Add(newProcesso);
            _uow.Complete();
        }

    }
}
