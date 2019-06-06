using GitProc.Data;
using GitProc.Model.Data;
using GitProc.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitProc.Services
{
    public class ProcessoService : IProcessoService
    {
        private readonly IUnitOfWork _uow;
        private readonly ITribunalService _tribunalService;

        public ProcessoService(IUnitOfWork uow, ITribunalService tribunalService)
        {
            _uow = uow;
            _tribunalService = tribunalService;
        }

        public async Task CreateProcessoAsync(Guid userId, string newProcesso)
        {
            Advogado advogado = await _uow.Advogado.SingleOrDefault(x => x.UsuarioId == userId);
            await _uow.Processo.Add(new Processo {
                Advogado = advogado,
                Numero = newProcesso,
                ProcessoId = new Guid(),
                DataAdicionado = DateTime.Now,
                Comarca = "A ser adicionado",
                AdvogadoId = advogado.AdvogadoId
            });
            _uow.Complete();

            await _tribunalService.GetOnlineProcessData(newProcesso);

        }

        public async Task<IEnumerable<Processo>> GetAllFromAdvogado(Guid AdvogadoId)
        {
            return await _uow.Processo.GetAll(x => x.AdvogadoId == AdvogadoId);
        }

    }
}
