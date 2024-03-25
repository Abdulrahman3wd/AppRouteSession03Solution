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
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected  readonly ApplecationDbContext _dbContext; // Null

        public GenericRepository(ApplecationDbContext dbContext) // Ask CLR for Creating Object from "AppDbContext"
        {
            _dbContext = dbContext;
        }



        public int Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
          //  _dbContext.Update(entity); // EF Core 3.1 New Feture 
            return _dbContext.SaveChanges();
        }
        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();

        }

        public int Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChanges();

        }

        public T Get(int id)
        {
            //var department = _dbContext.Departments.Local .Where(D=>D.Id == id).FirstOrDefault();
            //return department;
            return _dbContext.Set<T>().Find(id); // EF Core 3.1 Feature
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
                return (IEnumerable<T>) _dbContext.Employees.Include(E=>E.Department).AsNoTracking().ToList();
            else 
                return _dbContext.Set<T>().AsNoTracking().ToList();

        }
          

    }
}
