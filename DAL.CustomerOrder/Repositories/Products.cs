using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.CustomerOrderDemo.Repositories.DALObjects
{
	internal class Products
	{

		public int ProductID { get; set; }
		public string ProductName { get; set; }
		public float PACKHEIGHT { get; set; }
		public float PACKWIDTH { get; set; }
		public float PACKWEIGHT { get; set; }
		public string Colour { get; set; }
		public string Size { get; set; }


	}
	internal class Order
		{
		public int OrderID { get; set; }
		public string CustomerID { get; set; }
		public DateTime ORDERDATE { get; set; }
		public DateTime DeliveryExpected { get; set; }
	public bool containsGift { get; set; }
	public string ShippingMode { get; set; }
	public string OrderSource { get; set; }
		}

	internal class OrderItems
    {
		public int OrderItemID { get; set; }
public int OrderID { get; set; }
public int ProductID { get; set; }
public int QUANTITY { get; set; }
public float Proce { get; set; }
public bool Returnable { get; set; }


	}
}
