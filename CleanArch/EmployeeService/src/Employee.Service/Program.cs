using Employee.Application.Controllers;
using Employee.Application.Repository.Interfaces;
using Employee.Infrastructure.DbContexts;
using Employee.Interface;
using Employee.Service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<EmployeeDbContext>();
builder.Services.AddTransient<EmployeeController>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();

// Add services to the container.
builder.Services.AddGrpc(options =>
{
    options.MaxReceiveMessageSize = 1024 * 1024 * 1024;
    options.MaxSendMessageSize = 1024 * 1024 * 1024;
}
).AddJsonTranscoding();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<EmployeeService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
