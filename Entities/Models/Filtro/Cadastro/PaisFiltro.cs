using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Filtro.Cadastro
{
    public class PaisFiltro
    {
        public int tamanhoPagina { get; set; }
        public int numeroPagina { get; set; } = 0;
        public string nome { get; set; }
        public int continente { get; set; } = 5;
        public string colunaOrdem { get; set; }
        public string ordem { get; set; }
    }
}
