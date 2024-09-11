using Domain.Interfaces.Generics;
using Entities.Models.Cadastro;
using Entities.Models.Filtro.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastro
{
    public interface FormaPagamentoInterface : IGeneric<FormaPagamento>
    {
        public FormaPagamento GetFormaPagamento(string sigla, string descricao);
        public object Filtrar(FormaPagamentoFiltro filtro);
    }
}
