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
    public interface ContaInterface : IGeneric<Conta>
    {
        public Conta GetConta(string nroconta);
        public object Filtrar(ContaFiltro filtro);
    }
}
