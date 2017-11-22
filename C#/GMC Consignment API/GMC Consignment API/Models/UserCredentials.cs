using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMC_Consignment_API.Models
{
    public class UserCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string SKUMin { get; set; }
        public string SKUMax { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}