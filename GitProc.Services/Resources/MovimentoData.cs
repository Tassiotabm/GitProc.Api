using System;
using System.Collections.Generic;
using System.Text;

namespace GitProc.Services.Resources
{
    public class Movimento
    {
        public string MovimentoTitulo { get; set; }
        public List<string> MovimentoTag { get; set; }
        public List<string> MovimentoData { get; set; }
    }
}
