using App.DAL.Models;
using AppRouteSession03.BLL.Interfaces;
using AppRouteSession03.BLL.Repostories;
using AppRouteSession03.DAL.Models;
using AppRouteSession03.PL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRouteSession03.BLL
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly ApplecationDbContext _dbContext;
        //private Dictionary<string, IGenericRepository<ModelBase>> _repositories;
        private Hashtable _repositories;



        public UnitOfWork(ApplecationDbContext dbContext) // Ask Clr for creating object from (dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();

        }
        public IGenericRepository<T> Repository<T>() where T : ModelBase
        {
             
            var Key = typeof(T).Name;
            if (! _repositories.ContainsKey(Key))
            {
            
                if (Key == nameof(Employee))
                {
                  var  repository = new EmployeeRepository(_dbContext);
                    _repositories.Add(Key, repository);
                }
                else if (Key == nameof(Department))
                {
                   var repository = new DepartmentRepository(_dbContext);
                    _repositories.Add(Key, repository);
                }
               


            }
            return _repositories[Key] as IGenericRepository<T>;
        }
        public int Complete()
        {
            return _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Dispose(); // Close Connection
        }


    }
}
