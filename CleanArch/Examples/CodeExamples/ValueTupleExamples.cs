using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExamples
{
    internal class ValueTupleExamples
    {
        record struct Employee(int Id, string Name);
        record struct Department(int Id, string Name);

        static (Employee, Department) Main()
        {
            return (new Employee(), new Department(1, "abc"));
        }

        static void Main2()
        {
            var (emp, dep) = Main();
        }
    }
}
