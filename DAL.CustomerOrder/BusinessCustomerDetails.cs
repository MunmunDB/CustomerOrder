using DAL.CustomerOrderDemo.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DAL.CustomerOrderDemo
{
 
    /// <summary>
    /// This is a class to process the customer details from the external API
    /// This hold information about the API call 
    /// This also validates the customer by sending the email id & getting a customer detail json object return.
    /// </summary>
    internal class BusinessCustomerDetails {
        /// <summary>
        /// This is the method to consume external API which returns the customer information
        /// The API full test URL : https://customer-details.azurewebsites.net/api/GetUserDetails?email=sneeze@fake-customer.com&code=uu2ToG/dcsg3DI8CGlpLro1PyLhZNUWHpdPv8VmWFLBaxM0fvUZvkA==
        /// </summary>
        /// <returns></returns>
        public async Task<CustomerDetails> GetCustomerDetailsFromAPI(string email)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    //Build the URL for the Get API call
                    apiURL = apiURL + "&email=" + email;

                    using (HttpResponseMessage response = await httpClient.GetAsync(apiURL))
                    {

                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(apiResponse))
                        {
                            var customerlist = JsonSerializer.Deserialize<CustomerDetails>(apiResponse, new JsonSerializerOptions());
                            return customerlist;
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string apiURL = "https://customer-details.azurewebsites.net/api/GetUserDetails?code=uu2ToG/dcsg3DI8CGlpLro1PyLhZNUWHpdPv8VmWFLBaxM0fvUZvkA==";


    }
}


