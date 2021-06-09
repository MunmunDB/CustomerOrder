using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject_CustomerOrder_MMTDigital.Model
{
    public class CustomerInfo
    {
        [Required]
        public string ID { get; set; }

        [Required,EmailAddress]
        public string email { get; set; }

       
    }
    

}
