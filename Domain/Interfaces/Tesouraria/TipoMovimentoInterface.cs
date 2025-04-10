using Domain.Interfaces.Generics;
using Entities.Models.Filtro.Tesouraria;
using Entities.Models.Tesouraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Tesouraria
{
    public interface TipoMovimentoInterface : IGeneric<TipoMovimento>
    {
        public object Filtrar(TipoMovimentoFiltro filtro);
    }
}
