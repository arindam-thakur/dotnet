using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using Employee.Application.Domain;
using Employee.Application.Repository.Interfaces;
using Employee.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Employee.Interface;

public class EmployeeRepository : IEmployeeRepository
{
    readonly EmployeeDbContext _dbContext;

    public EmployeeRepository(EmployeeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ValueTask<bool> DeleteEmployee(Application.Domain.Employee employee)
    {
        return ValueTask.FromResult(true);
    }

    public ValueTask<Application.Domain.Employee> GetEmployeeById(int id)
    {
        var empEntity = _dbContext.EmployeeTable.AsNoTracking().FirstOrDefault(x => x.Id == id);

        var department = new Department(1, "Finance");
        var emp = new Employee.Application.Domain.Employee(empEntity.Id, empEntity.Name, empEntity.Address, department);
        return ValueTask.FromResult(emp);
    }

    public Task<ImmutableList<Application.Domain.Employee>> GetEmployees()
    {
        var immutableListBuilder = ImmutableList.CreateBuilder<Application.Domain.Employee>();

        var department = new Department(1, "Finance");
        var emp = new Employee.Application.Domain.Employee(1, "ABC", "DEF", department);

        immutableListBuilder.Add(emp);

        var immutableList = immutableListBuilder.ToImmutableList();
        // immutableList.Add(emp);

        return Task.FromResult(immutableList);
    }

    public Task<ReadOnlyCollection<Application.Domain.Employee>> GetEmployeesReadOnlyCollection()
    {
        var department = new Department(1, "Finance");
        var emp = new Employee.Application.Domain.Employee(1, "ABC", "DEF", department);

        var list = new List<Application.Domain.Employee>();
        list.Add(emp);

        var readonlyCol = list.AsReadOnly();

        return Task.FromResult(readonlyCol);
    }

    public Task<FrozenSet<Application.Domain.Employee>> GetEmployeesFrozenSet()
    {
        var department = new Department(1, "Finance");
        var emp = new Employee.Application.Domain.Employee(1, "ABC", "DEF", department);

        var list = new List<Application.Domain.Employee>();
        list.Add(emp);

        var frozen = list.ToFrozenSet();

        return Task.FromResult(frozen);
    }

    public ValueTask<bool> SaveEmployee(Application.Domain.Employee employee)
    {
        return ValueTask.FromResult(true);
    }
}
