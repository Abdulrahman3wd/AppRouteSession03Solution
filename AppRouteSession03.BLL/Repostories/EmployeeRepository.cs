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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplecationDbContext _dbContext;
        public EmployeeRepository(ApplecationDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        public int Add(Employee entity)
        {
            _dbContext.Employees.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(Employee entity)
        {
            _dbContext.Employees.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public Employee Get(int id)
        {
            return _dbContext.Find<Employee>(id);
        }

        public IEnumerable<Employee> GetAll()
            => _dbContext.Employees.AsNoTracking().ToList();

        public int Update(Employee entity)
        {
            _dbContext.Employees.Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
