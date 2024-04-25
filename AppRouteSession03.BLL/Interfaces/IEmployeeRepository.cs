using App.DAL.Models;
using AppRouteSession03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRouteSession03.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IQueryable<Employee> GetEmployeesByAdress(string address);
       IQueryable<Employee> SearchEmployeesByname(string name);

    }
}
