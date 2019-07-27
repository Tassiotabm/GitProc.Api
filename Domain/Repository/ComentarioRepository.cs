using GitProc.Data.Repository.Abstractions;
using GitProc.Model.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitProc.Data.Repository
{
    public class ComentarioRepository : Repository<DomainDbContext, Comentario>, IComentarioRepository
    {
        public ComentarioRepository(DomainDbContext context) : base(context)
        {

        }

        public async Task<List<Comentario>> GetAllComentarios(Guid processoId)
        {
            return await Context
                .Comentarios
                .Include(x=>x.Processo)
                .Where(x => x.ProcessoId == processoId)
                .OrderByDescending(x => x.ComentarioData)
                .ToListAsync();
        }
    }
}
