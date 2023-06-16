using Grpc.Core;
using Employee.Service;
using Employee.Application.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Employee.Service.Services;

public class EmployeeService : Employee.Service.EmployeeService.EmployeeServiceBase
{
    private readonly ILogger<EmployeeService> _logger;
    readonly EmployeeController _employeeController;

    public EmployeeService(ILogger<EmployeeService> logger, EmployeeController employeeController)
    {
        _logger = logger;
        _employeeController = employeeController;
    }

    //[ProducesResponseType(StatusCodes.Status200OK)]
    public override async Task<EmployeeResponse> GetEmployeeById(EmployeeRequest request, ServerCallContext context)
    {
        return await _employeeController.GetEmployeeById(request).ConfigureAwait(false);
    }
}
