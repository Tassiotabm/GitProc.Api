using GitProc.Data.Repository.Abstractions;
using GitProc.Model.Data;
using System.Threading.Tasks;

namespace GitProc.Data.Repository
{
    public class AdvogadoRepository : Repository<DomainDbContext, Advogado>, IAdvogadoRepository
    {
        public AdvogadoRepository(DomainDbContext context) : base(context)
        {

        }
    }
}
