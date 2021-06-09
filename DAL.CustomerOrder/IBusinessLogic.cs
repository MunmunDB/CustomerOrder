using DAL.CustomerOrderDemo.Repositories;
using System;

namespace DAL.CustomerOrderDemo
{
    /// <summary>
    /// This is the interface which allows to expose the business functionality to the Controller layer
    /// </summary>
    public interface IBusinessLogic
    {
       CustomerDetails ProcessRequest_validCustomerEmail(string ID, string email);

        CustomerOrder ProcessRequest_CheckForOrder(CustomerDetails cusInfo);
    }
}
