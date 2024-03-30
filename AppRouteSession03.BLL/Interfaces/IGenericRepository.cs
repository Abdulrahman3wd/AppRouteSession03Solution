using AppRouteSession03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRouteSession03.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T Employee);

        void Update(T Employee);

        void Delete(T entity);

    }
}
