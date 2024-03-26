using App.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Data.Configration
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {

        public void Configure(EntityTypeBuilder<Department> builder)
        {
            // fluent APIs for Department Domain 
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Code).HasColumnType("varchar").HasMaxLength(50).IsRequired(true);
            builder.Property(D => D.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired(true);


            builder.HasMany(D => D.Employees)
            .WithOne(E=>E.Department)
            .HasForeignKey(E => E.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

            }

    }
}
