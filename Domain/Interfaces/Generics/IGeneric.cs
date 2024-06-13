using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Generics
{
    public interface IGeneric<T>
    {
        public T Get(int id);
        public IEnumerable<T> GetAll();
        public T Insert(T entidade);
        public T Update(T entidade);
        public void Delete(T entidade);
    }
}
