using AppRouteSession03.BLL.Interfaces;
using AppRouteSession03.BLL.Repostories;
using AppRouteSession03.PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRouteSession03.BLL
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly ApplecationDbContext _dbContext;

        public IEmployeeRepository EmployeeRepository { get; set; } = null;
        public IDepartmentRepository DepartmentRepository { get; set; } = null;
        public UnitOfWork(ApplecationDbContext dbContext) // Ask Clr for creating object from (dbContext)
        {
            _dbContext = dbContext;
            EmployeeRepository = new EmployeeRepository(_dbContext);
            DepartmentRepository = new DepartmentRepository(_dbContext);
 


        }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Dispose(); 
        }
    }
}
