using App.DAL.Models;
using AppRouteSession03.BLL.Interfaces;
using AppRouteSession03.PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace AppRouteSession03.BLL.Repostories
{
    public class DepartmentRepository : GenericRepository<Department> , IDepartmentRepository
    {
        public DepartmentRepository(ApplecationDbContext dbContext):base(dbContext)
        {
            
        }

    }
}
