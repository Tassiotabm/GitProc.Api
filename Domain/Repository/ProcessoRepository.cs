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

        public async Task<List<Comentario>> GetComentarios(Guid processId) //Refazer
        {
            return await Context
                .Comentarios
                .Include(x => x.Advogado)
                .Where(x => x.ProcessoId == processId)
                .ToListAsync();
        }

        public async Task<List<Processo>> GetAllProcesso(Guid processMasterId) //Refazer
        {
            return await Context
                .Processos
                .Include(x=> x.Advogado)
                .Include(x=> x.Escritorio)
                .Where(x => x.ProcessoMasterId == processMasterId)              
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
