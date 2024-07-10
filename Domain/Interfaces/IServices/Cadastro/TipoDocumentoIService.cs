using Entities.Models.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IServices.Cadastro
{
    public interface TipoDocumentoIService
    {
        public TipoDocumento Get(int id);
        public IEnumerable<TipoDocumento> GetAll();
        public TipoDocumento Insert(TipoDocumento tipoDocumento);
        public TipoDocumento Update(TipoDocumento tipoDocumento);
        public void Delete(TipoDocumento tipoDocumento);
    }
}
