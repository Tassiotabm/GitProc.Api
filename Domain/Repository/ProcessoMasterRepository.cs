using GitProc.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitProc.Data.Repository
{
    public class ProcessoMasterRepository : Repository<DomainDbContext, ProcessoMaster>, IProcessoMasterRepository
    {
        public ProcessoMasterRepository(DomainDbContext context) : base(context)
        {

        }
    }
}
