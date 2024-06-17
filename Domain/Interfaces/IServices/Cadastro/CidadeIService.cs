using Entities.Models.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IServices.Cadastro
{
    public interface CidadeIService
    {
        public Cidade Get(int id);
        public IEnumerable<Cidade> GetAll();
        public Cidade Insert(Cidade cidade);
        public Cidade Update(Cidade cidade);
        public void Delete(Cidade cidade);
    }
}
