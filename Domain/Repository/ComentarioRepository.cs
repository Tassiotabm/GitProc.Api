using GitProc.Data.Repository.Abstractions;
using GitProc.Model.Data;

namespace GitProc.Data.Repository
{
    public class ComentarioRepository : Repository<DomainDbContext, Comentario>, IComentarioRepository
    {
        public ComentarioRepository(DomainDbContext context) : base(context)
        {

        }
    }
}
