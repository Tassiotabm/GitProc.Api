using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitProc.Model.Data
{
    public class ProcessoMaster
    {
        public Guid ProcessoMasterId { get; set; }
        public string NumeroProcesso { get; set; }
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
        public Advogado Advogado { get; set; }
        public Guid? AdvogadoId { get; set; }
        public string Nick { get; set; }
        public DateTime DataVerificacao { get; set; }
        public DateTime DataDistribuicao { get; set; }        
        public DateTime UpdatedDay { get; set; }
    }

    public class Movimento
    {
        public Guid MovimentoId { get; set; }
        public ProcessoMaster ProcessMaster { get; set; }
        public Guid ProcessMasterId { get; set; }
        public DateTime Data { get; set; }
        public string MovimentoTitulo { get; set; }
        [Column(TypeName = "jsonb")]
        public string MovimentoTag { get; set; }
        [Column(TypeName = "jsonb")]
        public string MovimentoData { get; set; }
    }
}
