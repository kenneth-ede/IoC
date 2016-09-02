using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using IOC;

namespace IOC
{

    public interface IILogger
    {

    }

    public class SqlServerLogger : IILogger
    {

    }

    public interface IRepository<T>
    {

    }

    public class SqlRepository<T> : IRepository<T>
    {
        public SqlRepository(IILogger logger)
        {

        }
    }

    public class InvoiceService
    {
        public InvoiceService(IRepository<Employee> repository, IILogger logger)
        {

        }
    }
}
