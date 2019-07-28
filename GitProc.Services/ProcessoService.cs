using GitProc.Data;
using GitProc.Data.Repository.Model;
using GitProc.Model.Data;
using GitProc.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitProc.Services
{
    public class ProcessoService : IProcessoService
    {
        private readonly IUnitOfWork _uow;
        private readonly ITribunalService _tribunalService;
        private readonly IEscritorioService _escritorioService;
        private readonly IAdvogadoService _advogadoService;

        public ProcessoService(IUnitOfWork uow,
            IEscritorioService escritorioService,
            IAdvogadoService advogadoService,
            ITribunalService tribunalService)
        {
            _uow = uow;
            _advogadoService = advogadoService;
            _escritorioService = escritorioService;
            _tribunalService = tribunalService;
        }

        public async Task AddProcesso(string comentario, string filePath, Processo processoData, string FileName)
        {
            var processo = new Processo();
            if (processoData.ProcessoId == null || processoData.ProcessoId == Guid.Empty)
            {
                var newGuid = Guid.NewGuid();
                await _uow.Processo.Add(new Processo
                {
                    ProcessoId = newGuid,
                    AdvogadoId = processoData.AdvogadoId,
                    EscritorioId = processoData.EscritorioId,
                    ProcessoMasterId = processoData.ProcessoMasterId,
                    DataAdicionado = DateTime.Now,
                    Nick = processoData.Nick
                });
                _uow.Complete();

                processo = await _uow.Processo.SingleOrDefault(x => x.ProcessoId == newGuid);
            }
            else
            {
                processo = await _uow.Processo.SingleOrDefault(x => x.ProcessoId == processoData.ProcessoId);
            }

            await _uow.Comentario.Add(new Comentario {
                    ComentarioData = comentario,
                    AdvogadoId = processoData.AdvogadoId,
                    File = filePath != "" ? System.IO.File.ReadAllBytes(filePath) : null,
                    FileName = FileName != ""? FileName : null,
                    ProcessoId = processo.ProcessoId,                    
            });
            _uow.Complete();

        }

        public async Task<IEnumerable<Comentario>> GetComentarios(Guid processId)
        {
            return await _uow.Processo.GetComentarios(processId);
        }

        public async Task<IEnumerable<Processo>> GetProcessos(Guid processoMasterId)
        {            
            return await _uow.Processo.GetAllProcesso(processoMasterId);
        }

        public async Task AddProcessMaster(Guid userId, string newProcesso, string nick)
        {
            var processExist = await _uow.ProcessoMaster.SingleOrDefault(x => x.NumeroProcesso == newProcesso);
            if (processExist != null)
            {
                await this.UpdateProcessAsync(processExist.ProcessoMasterId, newProcesso, nick);
                throw new InvalidOperationException("Processo ja existe!");
            }

            // CRIAR PROCESSO MASTER!!!!
            Advogado advogado = await _uow.Advogado.SingleOrDefault(x => x.UsuarioId == userId);
            await _tribunalService.GetOnlineProcessData(newProcesso, advogado.AdvogadoId, nick);
        }

        public async Task SaveMovimento(List<Movimento> lista)
        {
            await _uow.Movimento.AddRange(lista);
        }

        public async Task UpdateProcessAsync(Guid processoMasterId, string processNumber,string nick)
        {
            await _tribunalService.UpdateProcess(processoMasterId, processNumber, nick);
        }

        public async Task<IEnumerable<ProcessoMaster>> GetAllFromEscritorio(Guid userId)
        {
            var advogado = await _advogadoService.GetAdvogadoFromUserId(userId);
            return await _uow.Processo.GetAllEscrotorioInfo(advogado.Escritorio.EscritorioId);
        }

        public async Task<IEnumerable<ProcessoMaster>> GetAllFromAdvogado(Guid userId)
        {
            var advogado = await _advogadoService.GetAdvogadoFromUserId(userId);
            return await _uow.Processo.GetAllAdvogadoInfos(advogado.AdvogadoId);
        }

    }
}
