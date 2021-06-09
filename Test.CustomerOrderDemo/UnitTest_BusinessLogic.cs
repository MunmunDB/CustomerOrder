using DAL.CustomerOrderDemo;
using DAL.CustomerOrderDemo.Repositories;
using DemoProject_CustomerOrder_MMTDigital.Controllers;
using DemoProject_CustomerOrder_MMTDigital.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net;
using System.Web.Http.Results;

namespace Test.CustomerOrderDemo
{
    [TestClass]
    public class UnitTest_BusinessLogic
    {

        private readonly string connstr = "Server=tcp:mmt-sse-test.database.windows.net,1433;Initial Catalog=SSE_Test;Persist Security Info=False;User ID=mmt-sse-test;Password=database-user-01;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        [TestMethod]
        public void TestMethod_NullInput()
        {
            var controllerObj = new CustomerOrderController(null,null,null);
            var result = controllerObj.Post(null) as NoContentResult ;
            Assert.IsNotNull(result);
             Assert.AreEqual(204, result.StatusCode);
        }

        [TestMethod]
        public void TestMethod_InvalidCustomer()
        { var bllogger = new Mock<ILogger<BusinessLogic>>();
            var logger = new Mock<ILogger<CustomerOrderController>>();
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "Default")]).Returns(connstr);

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockConfSection.Object);

            var mockconnstr = mockConfiguration.Object.GetConnectionString("Default");

            var controllerObj = new CustomerOrderController(logger.Object, mockConfiguration.Object, new BusinessLogic(bllogger.Object, mockConfiguration.Object));

            var result = controllerObj.Post(new CustomerInfo
            {
                user = "bob@mmtdigital.co.uk",
                customerId = "R223232"
            }) as NotFoundObjectResult;
        Assert.IsNotNull(result);
        Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
    }

        [TestMethod]
        public void TestMethod_validCustomerEmail()
        {
            var testdata = new CustomerInfo
            {
                user = "santa@north-pole.lp.com",
                customerId = "XM45001"
            };
            var logger = new Mock<ILogger<CustomerOrderController>>();
            var config = new Mock<IConfiguration>();
            var bl = new Mock<IBusinessLogic>();
            bl.Setup(p => p.ProcessRequest_validCustomerEmail(testdata.customerId, testdata.user)).Returns(new CustomerDetails() { customerId= "XM45001", email= "santa@north-pole.lp.com",houseNumber="tt" });
            var controllerObj = new CustomerOrderController(logger.Object, config.Object, bl.Object);
            var result = controllerObj.Post(new CustomerInfo
            {
                user = "santa@north-pole.lp.com",
                customerId = "XM45001"
            }) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        }


        /// <summary>
        /// Mock the connection string in the config & test for a valid customer
        /// </summary>
        [TestMethod]
        public void TestMethod_ValidCustomerEmail()
        {
           
            var bllogger = new Mock<ILogger<BusinessLogic>>();
            var logger = new Mock<ILogger<CustomerOrderController>>();     
            

            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "Default")]).Returns(connstr);

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockConfSection.Object);

            var mockconnstr = mockConfiguration.Object.GetConnectionString("Default");

            var controllerObj = new CustomerOrderController(logger.Object, mockConfiguration.Object, new BusinessLogic(bllogger.Object, mockConfiguration.Object));
            var result = controllerObj.Post(new CustomerInfo { user = "cat.owner@mmtdigital.co.uk", customerId = "C34454" }) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        }
    }
}
