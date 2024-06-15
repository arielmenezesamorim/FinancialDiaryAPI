using Entities.Models.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IServices.Cadastro
{
    public interface UnidadeFederativaIService
    {
        public UnidadeFederativa Get(int id);
        public IEnumerable<UnidadeFederativa> GetAll();
        public UnidadeFederativa Insert(UnidadeFederativa unidadeFederativa);
        public UnidadeFederativa Update(UnidadeFederativa unidadeFederativa);
        public void Delete(UnidadeFederativa unidadeFederativa);
    }
}
