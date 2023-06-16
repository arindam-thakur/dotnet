using Employee.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Infrastructure.DbContexts
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<EmployeeEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {
            builder
                    .ToTable("ivp_srm_employee");
            builder
                    .Property(i => i.Id)
                    .HasColumnName("id")
                    .HasColumnName("int");

            builder
                    .HasKey(i => i.Id)
                    .HasName("PK_emp_id");
            builder
                    .Property(i => i.Name)
                    .HasColumnName("name")
                    .HasColumnType("nvarchar")
                    .HasMaxLength(100);
        }
    }

    public class DepartmentConfigurations : IEntityTypeConfiguration<DepartmentEntity>
    {
        public void Configure(EntityTypeBuilder<DepartmentEntity> builder)
        {
            builder
                    .ToTable("ivp_srm_department");
            builder
                    .Property(i => i.Id)
                    .HasColumnName("id")
                    .HasColumnName("int");

            builder
                    .HasKey(i => i.Id)
                    .HasName("PK_dept_id");
            builder
                    .Property(i => i.Name)
                    .HasColumnName("name")
                    .HasColumnType("nvarchar")
                    .HasMaxLength(100);
        }
    }
}
