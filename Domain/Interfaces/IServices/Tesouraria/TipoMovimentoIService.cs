using Entities.Models.Tesouraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IServices.Tesouraria
{
    public interface TipoMovimentoIService
    {
        public TipoMovimento Get(int id);
        public IEnumerable<TipoMovimento> GetAll();
        public TipoMovimento Insert(TipoMovimento tipoMovimento);
        public TipoMovimento Update(TipoMovimento tipoMovimento);
        public void Delete(TipoMovimento tipoMovimento);
    }
}
