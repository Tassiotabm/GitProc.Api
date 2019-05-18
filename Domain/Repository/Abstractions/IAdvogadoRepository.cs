using GitProc.Model.Data;
using System;
using System.Threading.Tasks;

namespace GitProc.Data.Repository.Abstractions
{
    public interface IAdvogadoRepository : IRepository<Advogado>
    {
        Task<Advogado> GetAllAdvogadoInfos(Guid userId);
    }
}
