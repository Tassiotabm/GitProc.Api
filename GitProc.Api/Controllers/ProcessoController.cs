using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitProc.Api.Models;
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

        public ProcessoController(
            IAdvogadoService advogadoService,
            IEscritorioService escritorioService,
            IUserService usuarioService,
            IProcessoService processoService)
        {
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

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
