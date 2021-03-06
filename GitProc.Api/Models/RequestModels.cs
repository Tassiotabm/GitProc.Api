﻿using GitProc.Model.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitProc.Api.Models
{
    public class RequestModels
    {
    }

    public class LoginModels
    {
        public string Password { get; set; } //temporario
        public string Email { get; set; }
    }

    public class RegisterModel
    {
        public string Password { get; set; }
        public string Login { get; set; }
        public string OAB { get; set; }
        public string CEP { get; set; }
        public string Name { get; set; }
        public Escritorio Escritorio { get; set; }
    }

    public class EscritorioModel
    {
        public Escritorio Escritorio { get; set; }
        public Guid AdvogadoId { get; set; }
    }

    public class ProcessModel
    {
        public string Nick { get; set; }
        public Guid UserId { get; set; }
        public string IdProcesso { get; set; }
    }

    public class NewProcessModel
    {
        public string Comentario { get; set; }
        public string Nick { get; set; }
        public IFormFile File { get; set; }
        public Guid AdvogadoId { get; set; }
        public Guid ProcessoMasterId { get; set; }
        public Guid EscritorioID { get; set; }
        public Guid ProcessoId { get; set; }
    }

    public class UpdateMasterProcess
    {
        public string Nick { get; set; }
        public Guid ProcessMasterValue { get; set; }
        public string ProcessNumber { get; set; }
    }
}
