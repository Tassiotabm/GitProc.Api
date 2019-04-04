using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitProc.Model.Data
{
    public class Escritorio
    {
        public Guid EscritorioId { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public string Endereco { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
