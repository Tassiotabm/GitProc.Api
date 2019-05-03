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
        public virtual Advogado Advogado { get; set; }
        public Guid? AdvogadoId { get; set; }
        public string Numero { get; set; }
        public string Comarca { get; set; }
        public DateTime DataAdicionado { get; set; }
        //a ser adicionado mais informacao
    }

    public class ProcessoVersionado
    {
        public Guid ProcessoVersionadoId { get; set; }

        //a ser adicionado mais informacao
    }
}
