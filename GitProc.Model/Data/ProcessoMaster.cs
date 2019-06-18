using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitProc.Model.Data
{
    public class ProcessoMaster
    {
        public Guid ProcessoMasterId { get; set; }
        public string ProcessoId { get; set; }
        public string Comarca { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Acao { get; set; }
        public string Classe { get; set; }
        public string Assunto { get; set; }
        public string Advogados { get; set; }
        public string Instancia { get; set; }
        public string Tribunal { get; set; }
        public List<Movimento> Movimentos { get; set; }
        public DateTime DataVerificacao { get; set; }
        public DateTime DataDistribuicao { get; set; }        
    }

    public class Movimento
    {
        public Guid MovimentoId { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public string ProcessoNoTribunal { get; set; }
        public string TipoMovimento { get; set; }
        public string Localizacao { get; set; }
    }
}
