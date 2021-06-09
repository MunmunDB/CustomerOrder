using DAL.CustomerOrderDemo.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DAL.CustomerOrderDemo
{
    /// <summary>
    /// This is the interface which allows to expose the business functionality to the Controller layer
    /// </summary>
    public class BusinessLogic : IBusinessLogic
    {
        private readonly ILogger<BusinessLogic> _logger;
        private readonly IDBContext _context;
        public BusinessLogic(ILogger<BusinessLogic> logger)
        { _logger = logger;

            
            _context = new DBContext();
        }
        public CustomerOrder ProcessRequest_CheckForOrder(CustomerDetails cusInfo)
        {

            var returnObj = new CustomerOrder();
            returnObj.customer = new Customer();
            returnObj.customer.firstName = cusInfo.firstName;
            returnObj.customer.firstName = cusInfo.lastName;

            var records = _context.GetLatestOrderDetailsForACustomer(cusInfo.customerId);
            foreach(DataRow rowitem in records.Rows)
            {

                if (returnObj.order==null )
                {
                    returnObj.order = new Order()
                    {
                        deliveryAddress = String.Concat(cusInfo.houseNumber, " ", cusInfo.street, " ", cusInfo.town, " ", cusInfo.postcode),
                        deliveryExpected = Convert.ToString(rowitem["DeliveryExpected"]),
                        orderDate = Convert.ToString(rowitem["OrderDate"]),
                        orderNumber = Convert.ToInt32(rowitem["OrderID"] ?? default(int)),
                        orderItems = new List<Product>()
                    };
                }
                returnObj.order.orderItems.Add(new Product() { product= Convert.ToString(rowitem["ProductName"]), quantity= Convert.ToInt32(rowitem["Quantity"]), priceEach= float.Parse(Convert.ToString(rowitem["Price"]??default(float))) });
            

            }

            return returnObj ;
        }

        public CustomerDetails ProcessRequest_validCustomerEmail(string ID, string email)
        {
            // Validate if the customer ID matches the email ID
            var cusBLobj = new BusinessCustomerDetails();
            Task<CustomerDetails> cusInfoobj=Task.Run(async () => { return await cusBLobj.GetCustomerDetailsFromAPI(email); });
           
            return cusInfoobj.Result;

        }

       
    }
}
