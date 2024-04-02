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
        private protected readonly ApplecationDbContext _dbContext; // Null

        public GenericRepository(ApplecationDbContext dbContext) // Ask CLR for Creating Object from "AppDbContext"
        {
            _dbContext = dbContext;
        }



        public  void Add(T entity)        
          => _dbContext.Set<T>().Add(entity);
          //  _dbContext.Update(entity); // EF Core 3.1 New Feture 
          
        public void Update(T entity)
           => _dbContext.Set<T>().Update(entity);
      

        

        public void Delete(T entity)       
          =>  _dbContext.Set<T>().Remove(entity);
           

        

        public async Task<T> GetAsync(int id)
        {
            //var department = _dbContext.Departments.Local .Where(D=>D.Id == id).FirstOrDefault();
            //return department;
            return await _dbContext.FindAsync<T>(id); // EF Core 3.1 Feature
        }

        public virtual async Task<IEnumerable<T>>  GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
                return (IEnumerable<T>) await _dbContext.Employees.Include(E => E.Department).AsNoTracking().ToListAsync();
            else
                return await _dbContext.Set<T>().AsNoTracking().ToListAsync();


        }
    }
}
