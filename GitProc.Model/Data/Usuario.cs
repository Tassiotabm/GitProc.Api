using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitProc.Model.Data
{
    public class Usuario
    {
        public Guid UsuarioId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
