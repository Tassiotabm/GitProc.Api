using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitProc.Model.Data
{
    public class Processo
    {
        public Guid ProcessoId { get; set; }
        public Advogado Advogado { get; set; }
        public Escritorio Escritorio { get; set; }
        public Guid? EscritorioId { get; set; }
        public Guid? AdvogadoId { get; set; }
        public string Numero { get; set; }
        public string Comarca { get; set; }
        public DateTime DataAdicionado { get; set; }
        public ProcessoMaster ProcessoMaster { get; set; }
        public Guid ProcessoMasterId { get; set; }
    }
}
