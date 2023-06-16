using Employee.Service;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Google.Rpc.Context.AttributeContext.Types;

namespace CodeExamples
{
    internal class RESTClient
    {
        static async ValueTask GetEmployeeById()
        {
            var request = new EmployeeRequest { EmployeeId = 10 };

            using var ms = new MemoryStream();
            await JsonSerializer.SerializeAsync(ms, request);

            ms.Seek(0, SeekOrigin.Begin);

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/v1/employeeservice");
            
            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (var requestContent = new StreamContent(ms))
            {
                httpRequest.Content = requestContent;
                requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                using (var response = await new HttpClient().SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStreamAsync();
                    var responseValue = await JsonSerializer.DeserializeAsync<EmployeeResponse>(content);
                }
            }
        }
    }
}
