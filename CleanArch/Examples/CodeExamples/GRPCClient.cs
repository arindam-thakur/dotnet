using Employee.Service;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExamples
{
    internal class GRPCClient
    {
        static async ValueTask GetEmployeeById()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5002");
            EmployeeService.EmployeeServiceClient client = new EmployeeService.EmployeeServiceClient(channel);

            var employeeResponse = await client.GetEmployeeByIdAsync(new EmployeeRequest { EmployeeId = 10 });
        }
    }
}
