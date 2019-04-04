using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitProc.Model.Data
{
    public class Versionamento
    {
        public Guid VersionamentoId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Processo ProcessoVersionado { get; set; }
        public Guid ProcessoId { get; set; }
        public int VersaoNumber { get; set; } 
     }
}
