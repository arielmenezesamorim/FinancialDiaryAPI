using Entities.Models.Tesouraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IServices.Tesouraria
{
    public interface ContaIService
    {
        public Conta Get(int id);
        public IEnumerable<Conta> GetAll();
        public Conta Insert(Conta conta);
        public Conta Update(Conta conta);
        public void Delete(Conta conta);
    }
}
