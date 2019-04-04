using GitProc.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitProc.Data.Repository
{
    public class ProcessoRepository : Repository<DomainDbContext, Processo>, IProcessoRepository
    {
        public ProcessoRepository(DomainDbContext context) : base(context)
        {

        }
    }
}
