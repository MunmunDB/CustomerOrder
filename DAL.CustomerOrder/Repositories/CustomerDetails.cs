using DocumentFormat.OpenXml.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Rewrite.Internal;
using Microsoft.Azure.Management.DataFactory.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace DAL.CustomerOrderDemo.Repositories
{
	/// <summary>
	/// This is to hold the customer details 
	/// This is accessible in the namespace DAL.CustomerOrder only
	/// </summary>
	public class CustomerDetails
	{
        public string email { get; set; }
		public string customerId { get; set; }
		public bool website { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }
		
		public string lastLoggedIn { get; set; }
		public string  houseNumber { get; set; }
		public string street { get; set; }
		public string town { get; set; }
		public string postcode { get; set; }
		public string preferredLanguage { get; set; }

	}
}
