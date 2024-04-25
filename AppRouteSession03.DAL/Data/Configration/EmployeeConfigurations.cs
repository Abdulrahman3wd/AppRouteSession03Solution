using AppRouteSession03.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRouteSession03.DAL.Data.Configration
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // Fluent APIs For Employee Domain

            builder.Property(E=>E.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(E => E.Address).IsRequired();
            builder.Property(E => E.Salary).HasColumnType("decimal(12,2)");
            builder.Property(E => E.gender)
                .HasConversion(
                (Gender) => Gender.ToString(),
                (GenderAsString) => (Gender)Enum.Parse(typeof(Gender), GenderAsString, true)

                );
            builder.Property(E => E.Name).IsRequired(true).HasMaxLength(50);

        }
    }
}
