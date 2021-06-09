using DAL.CustomerOrderDemo.Repositories;
using System;
using System.Threading.Tasks;

namespace DAL.CustomerOrderDemo
{
    /// <summary>
    /// This is the interface which allows to expose the business functionality to the Controller layer
    /// </summary>
    public class BusinessLogic : IBusinessLogic
    {
        public CustomerOrder ProcessRequest_CheckForOrder(CustomerDetails cusInfo)
        {

            var returnObj = new CustomerOrder();
            returnObj.customer.firstName = cusInfo.firstName;
            returnObj.customer.firstName = cusInfo.lastName;
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
