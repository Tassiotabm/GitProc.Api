using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GitProc.Api.Models;
using GitProc.Model.Data;
using GitProc.Services;
using GitProc.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GitProc.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProcessoController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAdvogadoService _advogadoService;
        private readonly IEscritorioService _escritorioService;
        private readonly IProcessoService _processService;
        private readonly IProcessoMasterService _processoMaster;

        public ProcessoController(
            IAdvogadoService advogadoService,
            IEscritorioService escritorioService,
            IUserService usuarioService,
            IProcessoMasterService processoMaster,
            IProcessoService processoService)
        {
            _processoMaster = processoMaster;
            _processService = processoService;
            _userService = usuarioService;
            _advogadoService = advogadoService;
            _escritorioService = escritorioService;
        }

        // GET api/<controller>/5
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            try
            {
                var List = await _processService.GetAllFromAdvogado(userId);
                return Ok(List);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<controller>/5
        [HttpGet("ByEscritorio/{userId}")]
        public async Task<IActionResult> GetByEscritorio(Guid userId)
        {
            try
            {
                var List = await _processService.GetAllFromEscritorio(userId);
                return Ok(List);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProcessModel Processo)
        {
            try
            {                
                await _processService.CreateProcessoAsync(Processo.UserId, Processo.IdProcesso);
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddComentario([FromForm]NewProcessModel formData)
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    await _processService.AddProcesso(formData.Comentario, fullPath, new Processo
                    {
                        AdvogadoId = formData.AdvogadoId,
                        EscritorioId = formData.EscritorioID,
                        ProcessoMasterId = formData.ProcessoMasterId,
                        ProcessoId = formData.ProcessoId
                    });
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("Movimentos/{ProcessoId}")]
        public async Task<IActionResult> GetMovimentos(Guid ProcessoId)
        {
            try
            {
                var movimentos = await _processoMaster.GetMovimento(ProcessoId);
                return Ok(movimentos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("Comentarios/{ProcessoId}")]
        public async Task<IActionResult> GetComentarios(Guid ProcessoId)
        {
            try
            {
                var movimentos = await _processService.GetComentarios(ProcessoId);
                return Ok(movimentos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("Processos/{ProcessoMasterId}")]
        public async Task<IActionResult> GetProcessos(Guid ProcessoMasterId)
        {
            try
            {
                var movimentos = await _processService.GetProcessos(ProcessoMasterId);
                return Ok(movimentos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("updateProcess/")]
        public async Task<IActionResult> Put([FromBody]UpdateMasterProcess processMaster)
        {
            try
            {
                await _processService.UpdateProcessAsync(processMaster.ProcessMasterValue,processMaster.ProcessNumber);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);

            }
        }

        
    }
}
