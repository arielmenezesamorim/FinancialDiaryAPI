using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Filtro.Tesouraria
{
    public class TipoMovimentoFiltro
    {
        public int tamanhoPagina { get; set; }
        public int numeroPagina { get; set; } = 0;
        public string descricao { get; set; }
        public string colunaOrdem { get; set; }
        public string ordem { get; set; }
        public int tipo { get; set; } = -1;
        public int indicador { get; set; } = -1;
    }
}
