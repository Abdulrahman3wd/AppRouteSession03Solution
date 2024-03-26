using App.DAL.Models;
using AppRouteSession03.BLL.Interfaces;
using AppRouteSession03.DAL.Models;
using AppRouteSession03.PL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRouteSession03.BLL.Repostories
{
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
    {
       

        public EmployeeRepository(ApplecationDbContext dbContext):base(dbContext) // Ask CLR for Creating object From DbContext
        {
            dbContext = _dbContext;
          
        }


        public IQueryable<Employee> GetEmployeesByAdress(string address)
        {
            return _dbContext.Employees.Where(E => E.Address.ToLower() == address.ToLower());
        }

        public IQueryable<Employee> SearchEmployeesByname(string name)
        => _dbContext.Employees.Where(E => E.Name.ToLower().Contains(name));
            
        
    }
}
