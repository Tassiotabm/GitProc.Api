using GitProc.Data.Repository.Abstractions;
using GitProc.Model.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GitProc.Data.Repository
{
    public class AdvogadoRepository : Repository<DomainDbContext, Advogado>, IAdvogadoRepository
    {
        public AdvogadoRepository(DomainDbContext context) : base(context)
        {

        }

        public async Task<Advogado> GetAllAdvogadoInfos(Guid userId)
        {
            return await Context.Advogados.Include(x => x.Escritorio).Where(x => x.UsuarioId == userId).FirstOrDefaultAsync();
        }
    }
}
