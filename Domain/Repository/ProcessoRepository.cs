using GitProc.Data.Repository.Model;
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

        public async Task<List<ProcessoData>> GetAllProcesso(Guid processMasterId)
        {
            return await Context
                .Processos
                .Where(x => x.ProcessoMasterId == processMasterId)
                .Join(Context.Comentarios,e=> e.ProcessoId,p=> p.ProcessoId,(p,e) => new ProcessoData
                {
                    Processo =  p,
                    Comentario = e
                } )                
                .ToListAsync();
        }

        public async Task<List<ProcessoMaster>> GetAllAdvogadoInfos(Guid advogadoId)
        {
            return await Context
                .ProcessoMaster
                .Include(x=> x.Advogado)
                .Where(x => x.AdvogadoId == advogadoId).ToListAsync();
        }

        public async Task<List<ProcessoMaster>> GetAllEscrotorioInfo(Guid escritorioId)
        {
            return await Context
                .ProcessoMaster
                .Include(x => x.Advogado)
                .Where(x => x.Advogado.EscritorioId == escritorioId).ToListAsync();
        }
    }
}
