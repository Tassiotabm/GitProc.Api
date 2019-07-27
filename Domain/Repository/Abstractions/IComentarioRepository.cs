using GitProc.Model.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitProc.Data.Repository.Abstractions
{
    public interface IComentarioRepository : IRepository<Comentario>
    {
        Task<List<Comentario>> GetAllComentarios(Guid processoId);
    }
}
