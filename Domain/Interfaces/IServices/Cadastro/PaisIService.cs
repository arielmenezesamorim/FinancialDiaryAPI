using Entities.Models.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IServices.Cadastro
{
    public interface PaisIService
    {
        public Pais Get(int id);
        public IEnumerable<Pais> GetAll();
        public Pais Insert(Pais pais);
        public Pais Update(Pais pais);
        public void Delete(Pais pais);
    }
}
