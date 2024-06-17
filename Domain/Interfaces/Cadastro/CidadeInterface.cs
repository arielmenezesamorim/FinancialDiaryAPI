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
    public interface CidadeInterface : IGeneric<Cidade>
    {
        public Cidade GetCidade(int sequencia);
        public object Filtrar(CidadeFiltro filtro);
        public IEnumerable<Cidade> GetAllPorUf(string sigla);
    }
}
