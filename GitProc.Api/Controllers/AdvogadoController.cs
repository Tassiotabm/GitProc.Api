using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitProc.Model.Data;
using GitProc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GitProc.Api.Controllers
{
    [Route("api/[controller]")]
    public class AdvogadoController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAdvogadoService _advogadoService;
        private readonly IEscritorioService _escritorioService;

        public AdvogadoController(
            IAdvogadoService advogadoService,
            IEscritorioService escritorioService,
            IUserService usuarioService)
        {
            _userService = usuarioService;
            _advogadoService = advogadoService;
            _escritorioService = escritorioService;
        }

        [AllowAnonymous]
        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] Escritorio model)
        {
            try
            {
                await _escritorioService.CreateEscritorio(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            try
            {
                Advogado advogadoData = await _advogadoService.GetAdvogadoFromUserId(userId);
                return Ok(advogadoData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
