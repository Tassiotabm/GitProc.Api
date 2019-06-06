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
    public class ProcessoMasterService : IProcessoMasterService
    {
        private readonly IUnitOfWork _uow;

        public ProcessoMasterService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void SaveProcessoMaster(ProcessoMaster processo)
        {
            _uow.ProcessoMaster.Add(processo);
        }
    }
}
