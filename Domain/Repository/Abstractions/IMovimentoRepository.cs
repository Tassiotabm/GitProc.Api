using GitProc.Model.Data;
using System;
using System.Threading.Tasks;

namespace GitProc.Data.Repository.Abstractions
{
    public interface IMovimentoRepository : IRepository<Movimento>
    {
        Task<Movimento> GetFirstMovimento(Guid procMasterId);
    }
}
