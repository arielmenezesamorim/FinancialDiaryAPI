using Entities.Models.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IServices.Cadastro
{
    public interface FormaPagamentoIService
    {
        public FormaPagamento Get(int id);
        public IEnumerable<FormaPagamento> GetAll();
        public FormaPagamento Insert(FormaPagamento formaPagamento);
        public FormaPagamento Update(FormaPagamento formaPagamento);
        public void Delete(FormaPagamento formaPagamento);
    }
}
