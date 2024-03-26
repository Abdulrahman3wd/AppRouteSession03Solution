using App.DAL.Models;
using AppRouteSession03.BLL.Interfaces;
using AppRouteSession03.PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppRouteSession03.DAL.Models;
namespace AppRouteSession03.BLL.Repostories
{
    public class DepartmentRepository : GenericRepository<Department> , IDepartmentRepository
    {
        public DepartmentRepository(ApplecationDbContext dbContext):base(dbContext)
        {
            dbContext = _dbContext;

        }

        public IQueryable<Department> SearchDepartmentByname(string name)
            => _dbContext.Departments.Where(E => E.Name.ToLower().Contains(name));

    }
}
