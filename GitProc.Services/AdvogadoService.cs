using System;
using System.Threading.Tasks;
using GitProc.Data;
using GitProc.Model.Data;

namespace GitProc.Services
{
    public class AdvogadoService : IAdvogadoService
    {
        private readonly IUnitOfWork _uow;

        public AdvogadoService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task CreateAdvogado(Advogado advogado)
        {
            advogado.CreatedAt = DateTime.Now;
            await _uow.Advogado.Add(advogado);
            _uow.Complete();
        }

        public async Task<Advogado> GetAdvogadoFromUserId(Guid userId)
        {
            return await _uow.Advogado.GetAllAdvogadoInfos(userId);
        }

        public async Task RemoveAdvogado(Guid advogadoId)
        {
            Advogado advogado = await _uow.Advogado.SingleOrDefault(x => x.AdvogadoId == advogadoId);
            _uow.Advogado.Remove(advogado);
            _uow.Complete();
        }

        public async Task EditAdvogado(Advogado advogado, Guid adovogadoId)
        {
            Advogado advogadoToBeEdited = await _uow.Advogado.SingleOrDefault(x => x.AdvogadoId == adovogadoId);
            _uow.Advogado.Attach(advogadoToBeEdited);
            advogadoToBeEdited.Escritorio = advogado.Escritorio;
            advogadoToBeEdited.Name = advogado.Name;
            advogadoToBeEdited.OAB = advogado.OAB;
            _uow.Complete();
        }
    }
}
