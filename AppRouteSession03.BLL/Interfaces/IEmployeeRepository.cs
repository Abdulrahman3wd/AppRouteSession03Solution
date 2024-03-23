using App.DAL.Models;
using AppRouteSession03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRouteSession03.BLL.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee Get(int id);
        int Add(Employee Employee);

        int Update(Employee Employee);

        int Delete(Employee entity);
    }
}
