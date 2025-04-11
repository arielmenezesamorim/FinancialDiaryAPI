using Domain.Interfaces.Generics;
using Entities.Models.Cliente;
using Entities.Models.Filtro.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cliente
{
    public interface TipoClienteInterface : IGeneric<TipoCliente>
    {
        public object Filtrar(TipoClienteFiltro filtro);
    }
}
