using System;
using IOC;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class IoCTests
    {
        [TestMethod]
        public void Can_reslove_Types()
        {
            var ioc = new Container();

            ioc.For<IILogger>().Use<SqlServerLogger>();

            var logger = ioc.Resolve<IILogger>();

            Assert.AreEqual(typeof (SqlServerLogger), logger.GetType());
        }

        [TestMethod]
        public void Can_reslove_TypesWithoutDafaultCtor()
        {
            var ioc = new Container();

            ioc.For<IILogger>().Use<SqlServerLogger>();
            ioc.For<IRepository<Employee>>().Use<SqlRepository<Employee>>();

            var repository = ioc.Resolve<IRepository<Employee>>();

            Assert.AreEqual(typeof (SqlRepository<Employee>), repository.GetType());
        }

        [TestMethod]
        public void Can_reslove_ConcreteType()
        {
            var ioc = new Container();

            ioc.For<IILogger>().Use<SqlServerLogger>();
            ioc.For<IRepository<Employee>>().Use<SqlRepository<Employee>>();

            var service = ioc.Resolve<InvoiceService>();

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Can_reslove_MultipleGenericTypes() {
            var ioc = new Container();

            ioc.For<IILogger>().Use<SqlServerLogger>();
            ioc.For(typeof(IRepository<>)).Use(typeof(SqlRepository<>));

            var service = ioc.Resolve<InvoiceService>();

            Assert.IsNotNull(service);
        }
    }
}
