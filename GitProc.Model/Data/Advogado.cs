using System;
using System.Collections.Generic;
using System.Text;

namespace GitProc.Model.Data
{
    public class Advogado
    {
        public Guid AdvogadoId { get; set; }
        public Usuario Usuario { get; set; }
        public Guid UsuarioId { get; set; }
        public string OAB { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Escritorio Escritorio { get; set; }
        public Guid? EscritorioId { get; set; }
    }
}
