using GitProc.Data;
using GitProc.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitProc.Services
{
    public class EscritorioService : IEscritorioService
    {
        private readonly IUnitOfWork _uow;

        public EscritorioService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Escritorio> CreateEscritorio(Escritorio escritorio,Guid advogadoId)
        {
            if (escritorio.EscritorioId == Guid.Empty)
            {
                await _uow.Escritorio.Add(escritorio);
                _uow.Complete();
                Advogado advogado = await _uow.Advogado.SingleOrDefault(x => x.UsuarioId == advogadoId);
                advogado.Escritorio = escritorio;
                _uow.Complete();
            }
            else
                await EditEscritorio(escritorio);
            
            return escritorio;
        }

        public async Task RemoveEscritorio(Guid escritorioId)
        {
            Escritorio escritorio = await _uow.Escritorio.SingleOrDefault(x => x.EscritorioId == escritorioId);
            _uow.Escritorio.Remove(escritorio);
            _uow.Complete();
        }

        public async Task EditEscritorio(Escritorio escritorio)
        {
            Escritorio escritorioToBeEdited = await _uow.Escritorio.SingleOrDefault(x => x.EscritorioId == escritorio.EscritorioId);
            _uow.Escritorio.Attach(escritorioToBeEdited);
            escritorioToBeEdited.Endereco = escritorio.Endereco;
            escritorioToBeEdited.CNPJ = escritorio.CNPJ;
            escritorioToBeEdited.Name = escritorio.Name;
            _uow.Complete();
        }

    }
}
