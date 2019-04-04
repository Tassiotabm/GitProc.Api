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

        public async Task<Escritorio> CreateEscritorio(Escritorio escritorio)
        {
            await _uow.Escritorio.Add(escritorio);
            _uow.Complete();
            return escritorio;
        }

        public async Task RemoveEscritorio(Guid escritorioId)
        {
            Escritorio escritorio = await _uow.Escritorio.SingleOrDefault(x => x.EscritorioId == escritorioId);
            _uow.Escritorio.Remove(escritorio);
            _uow.Complete();
        }

        public async Task EditEscritorio(string endereco, string name, string CNPJ, Guid escritorioId)
        {
            Escritorio escritorioToBeEdited = await _uow.Escritorio.SingleOrDefault(x => x.EscritorioId == escritorioId);
            _uow.Escritorio.Attach(escritorioToBeEdited);
            escritorioToBeEdited.Endereco = endereco;
            escritorioToBeEdited.CNPJ = CNPJ;
            escritorioToBeEdited.Name = name;
            _uow.Complete();
        }

    }
}
