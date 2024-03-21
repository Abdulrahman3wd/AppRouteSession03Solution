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
    internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplecationDbContext _dbContext; // Null

        public DepartmentRepository(ApplecationDbContext dbContext) // Ask CLR for Creating Object from "AppDbContext"
        {
            _dbContext = dbContext;
        }



        public int Add(Department entity)
        {
            _dbContext.Departments.Add(entity);
             return  _dbContext.SaveChanges();
        }
        public int Update(Department entity)
        {
            _dbContext.Departments.Update(entity);
            return _dbContext.SaveChanges();

        }

        public int Delete(Department entity)
        {
            _dbContext.Departments.Remove(entity);
            return _dbContext.SaveChanges();

        }

        public Department Get(int id)
        {
            //var department = _dbContext.Departments.Local .Where(D=>D.Id == id).FirstOrDefault();
            //return department;
            return _dbContext.Find<Department>(id); // EF Core 3.1 Feature
        }

        public IEnumerable<Department> GetAll()       
            => _dbContext.Departments.AsNoTracking().ToList();

    }
}
