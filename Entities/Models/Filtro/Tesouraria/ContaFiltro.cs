using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Filtro.Tesouraria
{
    public class ContaFiltro
    {
        public int tamanhoPagina { get; set; }
        public int numeroPagina { get; set; } = 0;
        public string colunaOrdem { get; set; }
        public string ordem { get; set; }
        public string descricao { get; set; }
        public int tipoConta { get; set; } = -1;
    }
}
