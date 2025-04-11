using Entities.Models.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IServices.Cliente
{
    public interface TipoClienteIService
    {
        public TipoCliente Get(int id);
        public IEnumerable<TipoCliente> GetAll();
        public TipoCliente Insert(TipoCliente tipoCliente);
        public TipoCliente Update(TipoCliente tipoCliente);
        public void Delete(TipoCliente tipoCliente);
    }
}
