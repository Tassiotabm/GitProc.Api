using GitProc.Data.Repository.Abstractions;
using GitProc.Model.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GitProc.Data.Repository
{
    public class MovimentoRepository : Repository<DomainDbContext, Movimento>, IMovimentoRepository
    {
        public MovimentoRepository(DomainDbContext context) : base(context)
        {

        }

        public async Task<Movimento> GetFirstMovimento(Guid procMasterId)
        {
            return await Context
                .Movimentos
                .Where(x => x.ProcessMasterId == procMasterId)
                .OrderByDescending(x => x.Data)
                .FirstAsync();
        }
    }
}
