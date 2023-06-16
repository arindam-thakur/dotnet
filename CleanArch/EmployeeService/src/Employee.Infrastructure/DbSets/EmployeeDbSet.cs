using Employee.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employee.Infrastructure.DbContexts
{
    public partial class EmployeeDbContext
    {
        public DbSet<EmployeeEntity> EmployeeTable { get; set; }
        public DbSet<DepartmentEntity> DepartmentTable { get; set; }
    }
}
