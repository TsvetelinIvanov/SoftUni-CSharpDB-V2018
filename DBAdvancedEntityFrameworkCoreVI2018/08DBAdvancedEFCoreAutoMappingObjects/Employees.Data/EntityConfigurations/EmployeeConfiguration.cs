using Employees.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employees.Data.EntityConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.FirstName).IsUnicode();

            builder.Property(e => e.LastName).IsUnicode();

            builder.Property(e => e.Address).IsUnicode();

            builder.HasOne(e => e.Manager).WithMany(e => e.ManagerEmployees).HasForeignKey(e => e.ManagerId);
        }
    }
}