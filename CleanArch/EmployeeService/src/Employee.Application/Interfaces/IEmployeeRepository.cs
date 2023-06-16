namespace Employee.Application.Repository.Interfaces;
using Employee.Application.Domain;
using System.Collections.ObjectModel;

public interface IEmployeeRepository
{
    ValueTask<Employee> GetEmployeeById(int id);
    Task<ReadOnlyCollection<Application.Domain.Employee>> GetEmployeesReadOnlyCollection();
    ValueTask<bool> SaveEmployee(Employee employee);
    ValueTask<bool> DeleteEmployee(Employee employee);
}
