﻿using GitProc.Data;
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

        public async Task AddProcesso(string comentario, string filePath, Processo processoData)
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
                    Numero = processoData.Numero
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
                    File = System.IO.File.ReadAllBytes(filePath),
                    ProcessoId = processo.ProcessoId,                    
            });
            _uow.Complete();

        }

        public async Task CreateProcessoAsync(Guid userId, string newProcesso)
        {
            var processExist = await _uow.ProcessoMaster.SingleOrDefault(x => x.NumeroProcesso == newProcesso);
            if (processExist != null)
            {
                await this.UpdateProcessAsync(processExist.ProcessoMasterId, newProcesso);
                throw new InvalidOperationException("Processo ja existe!");
            }

            // CRIAR PROCESSO MASTER!!!!
            Advogado advogado = await _uow.Advogado.SingleOrDefault(x => x.UsuarioId == userId);
            var master = await _tribunalService.GetOnlineProcessData(newProcesso);

            await _uow.Processo.Add(new Processo {
                Advogado = advogado,
                Numero = newProcesso,
                ProcessoId = new Guid(),
                DataAdicionado = DateTime.Now,
                Comarca = "A ser adicionado",
                EscritorioId = advogado.EscritorioId,
                ProcessoMasterId = master.ProcessoMasterId,
                ProcessoMaster = master,
                AdvogadoId = advogado.AdvogadoId
            });
            _uow.Complete();

        }

        public async Task SaveMovimento(List<Movimento> lista)
        {
            await _uow.Movimento.AddRange(lista);
        }

        public async Task UpdateProcessAsync(Guid processoMasterId, string processNumber)
        {
            await _tribunalService.UpdateProcess(processoMasterId, processNumber);
        }

        public async Task<IEnumerable<Processo>> GetAllFromEscritorio(Guid userId)
        {
            var advogado = await _advogadoService.GetAdvogadoFromUserId(userId);
            return await _uow.Processo.GetAllEscrotorioInfo(advogado.Escritorio.EscritorioId);
        }

        public async Task<IEnumerable<Processo>> GetAllFromAdvogado(Guid userId)
        {
            var advogado = await _advogadoService.GetAdvogadoFromUserId(userId);
            return await _uow.Processo.GetAllAdvogadoInfos(advogado.AdvogadoId);
        }

    }
}
