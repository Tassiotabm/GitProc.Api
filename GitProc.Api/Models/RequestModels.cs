using GitProc.Model.Data;
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
}
