using Employee.Application.Repository.Interfaces;
using Employee.Service;
using Microsoft.Extensions.Logging;

namespace Employee.Application.Controllers;

public class EmployeeController
{
    readonly ILogger<EmployeeController> _logger;
    readonly IEmployeeRepository _employeeRepository;

    public EmployeeController(ILogger<EmployeeController> logger, IEmployeeRepository employeeRepository)
    {
        _logger = logger;
        _employeeRepository = employeeRepository;
    }
    public async Task<EmployeeResponse> GetEmployeeById(EmployeeRequest request)
    {
        _logger.LogInformation("GetEmployeeById -> START");
        try
        {
            var employee = await _employeeRepository.GetEmployeeById(request.EmployeeId).ConfigureAwait(false);
            return new EmployeeResponse { EmployeeName = employee.Name, EmployeeAddress = employee.Address };
        }
        finally
        {
            _logger.LogInformation("GetEmployeeById -> END");
        }
    }
}