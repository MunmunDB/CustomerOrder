﻿using DAL.CustomerOrderDemo;
using DemoProject_CustomerOrder_MMTDigital.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoProject_CustomerOrder_MMTDigital.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerOrderController : ControllerBase
    {
       
        private readonly ILogger<CustomerOrderController> _logger;
        private readonly IConfiguration _configuration;

        public CustomerOrderController(ILogger<CustomerOrderController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        /// This is the test method to check its LIVE
        /// Ignore
        public IEnumerable<dynamic> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new             {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55)
                
            })
            .ToArray();
        }

        
       [HttpPost]
       [Produces("application/json")]
       ///This is a POST API method
       ///to accept CustomerInfo{ID,email}  as JSON inpput
       ///to validate & return a response with all the latest orders for the customer ID mentioned in the JSON
        public IActionResult Post(CustomerInfo cus)
        {
            
            IBusinessLogic repo = new BusinessLogic();
            
            // 1. Validate the customer 
            var CusInfo = repo.ProcessRequest_validCustomerEmail(cus.ID, cus.email);
             
            if(CusInfo==null || cus.ID!=CusInfo.customerId)
                return NotFound(cus);

            //2. Check for a order 
            var customerOrder=repo.ProcessRequest_CheckForOrder(CusInfo);

            //3. Check for a order with gift

            return Ok(customerOrder);

            
        }


    }
}