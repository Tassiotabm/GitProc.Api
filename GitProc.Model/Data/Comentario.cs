﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitProc.Model.Data
{
    public class Comentario
    {
        public Guid ComentarioId {get;set;}
        public string ComentarioData { get; set; }
        public Processo Processo { get; set; }
        public Guid ProcessoId { get; set; }
        public byte[] File { get; set; }
        public DateTime DataCriado { get; set; }
    }
}
