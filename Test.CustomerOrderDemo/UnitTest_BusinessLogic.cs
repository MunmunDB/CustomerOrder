using DemoProject_CustomerOrder_MMTDigital.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var result = controllerObj.Post(null) ;
            Assert.IsNotNull(result);
           // Assert.AreEqual(HttpStatusCode.NoContent, result);
        }
    }
}
