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
            var config = new Mock<IConfiguration>();
           
            var controllerObj = new CustomerOrderController(logger.Object, config.Object, new BusinessLogic(bllogger.Object));

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
        [TestMethod]
        public void TestMethod_ValidCustomerEmail()
        {
            var bllogger = new Mock<ILogger<BusinessLogic>>();
            var logger = new Mock<ILogger<CustomerOrderController>>();
            var config = new Mock<IConfiguration>();

            var controllerObj = new CustomerOrderController(logger.Object, config.Object, new BusinessLogic(bllogger.Object));
            var result = controllerObj.Post(new CustomerInfo { user = "cat.owner@mmtdigital.co.uk", customerId = "C34454" }) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        }
    }
}
