using GitProc.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitProc.Data.Repository
{
    public class EscritorioRepository : Repository<DomainDbContext, Escritorio>, IEscritorioRepository
    {
        public EscritorioRepository(DomainDbContext context) : base(context)
        {

        }
    }
}
