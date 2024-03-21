using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using App.DAL.Models;

namespace AppRouteSession03.PL
{
    public class ApplecationDbContext : DbContext
    {

        public ApplecationDbContext(DbContextOptions<ApplecationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
        public DbSet<Department> Departments { get; set; }
    }

}
