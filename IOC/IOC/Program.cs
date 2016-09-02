using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IOC {
    class Program {
        static void Main(string[] args)
        {
            var employeeList = CreateCollection(typeof(List<>), typeof(Employee));

            Console.WriteLine(employeeList.GetType().Name);

            var genericArguments = employeeList.GetType().GenericTypeArguments;

            foreach (var arg in genericArguments)
            {
                Console.WriteLine("[{0}]", arg.Name);
            }

            var employee = new Employee();
            var employeeType = typeof (Employee);
            var methodInfo = employeeType.GetMethod("Speak").MakeGenericMethod(typeof(DateTime));

            methodInfo.Invoke(employee, null);

            Console.ReadLine();
        }

        private static object CreateCollection(Type collectionType, Type itemType)
        {
            var closedType = collectionType.MakeGenericType(itemType);

            return Activator.CreateInstance(closedType);
        }
    }

    public class Employee
    {
        public string Name { get; set; }

        public void Speak<T>()
        {
            Console.WriteLine(typeof(T).Name);
        }
    }
}
