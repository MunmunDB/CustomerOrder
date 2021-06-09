using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.CustomerOrderDemo.Repositories
{
    public class Product
    {

        public string product { get; set; }
        public int quantity { get; set; }
        public float priceEach { get; set; }
    }
    public class Order
    {
        public int orderNumber { get; set; }
        public string orderDate { get; set; }
        public string deliveryAddress { get; set; }
        public string deliveryExpected { get; set; }
        public IList<Product> orderItems { get; set; }
    }

    public class Customer
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    /// <summary>
    /// This is the RootClass structure used by the API to return the mentioned format
    /// </summary>
    public class CustomerOrder
    {
        public Customer customer { get; set; }
        public Order order { get; set; }
        
    }
}
