using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitProc.Api.Models;
using GitProc.Data.Repository;
using GitProc.Data.Repository.Abstractions;
using GitProc.Model.Data;
using GitProc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GitProc.Api.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IAdvogadoService _advogadoService;

        public LoginController(
            IAdvogadoService advogadoService,
            IUserService usuarioService)
        {
            _userService = usuarioService;
            _advogadoService = advogadoService;
        }

        [AllowAnonymous]
        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] LoginModels model)
        {
            try
            {
                Usuario user = await _userService.Login(model.Password, model.Email);
                if (user != null)
                    return Ok(user.UsuarioId);
                else
                    return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterModel model)
        {
            try
            {
                var User = await _userService.CreateUser(new Usuario { Login = model.Login, Password = model.Password});
                if (User == null)
                    return BadRequest("Usuário já existe!");
                await _advogadoService.CreateAdvogado(new Advogado { OAB = model.OAB, Usuario = User, Escritorio = model.Escritorio, Name = model.Name });
                return Ok(User.UsuarioId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
