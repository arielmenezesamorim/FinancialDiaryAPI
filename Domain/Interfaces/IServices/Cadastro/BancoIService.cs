using Entities.Models.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IServices.Cadastro
{
    public interface BancoIService
    {
        public Banco Get(int id);
        public IEnumerable<Banco> GetAll();
        public Banco Insert(Banco banco);
        public Banco Update(Banco banco);
        public void Delete(Banco banco);
    }
}
