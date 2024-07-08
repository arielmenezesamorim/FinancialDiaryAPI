using Entities.Models.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IServices.Cadastro
{
    public interface DepartamenteIService
    {
        public Departamento Get(int id);
        public IEnumerable<Departamento> GetAll();
        public Departamento Insert(Departamento departamento);
        public Departamento Update(Departamento departamento);
        public void Delete(Departamento departamento);
    }
}
