using GitProc.Model.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitProc.Data.Repository
{
    public class ProcessoRepository : Repository<DomainDbContext, Processo>, IProcessoRepository
    {
        public ProcessoRepository(DomainDbContext context) : base(context)
        {

        }

        public async Task<List<Processo>> GetAllAdvogadoInfos(Guid advogadoId)
        {
            return await Context.Processos.Include(x => x.ProcessoMaster).Include(x=> x.Advogado).Include(x=> x.Escritorio).Where(x => x.AdvogadoId == advogadoId).ToListAsync();
        }

        public async Task<List<Processo>> GetAllEscrotorioInfo(Guid escritorioId)
        {
            return await Context.Processos.Include(x => x.ProcessoMaster).Include(x => x.Advogado).Include(x => x.Escritorio).Where(x => x.EscritorioId == escritorioId).ToListAsync();
        }
    }
}
