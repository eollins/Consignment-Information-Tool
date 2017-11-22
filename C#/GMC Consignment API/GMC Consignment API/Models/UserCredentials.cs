using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMC_Consignment_API.Models
{
    public class UserCredentials
    {
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static string Name { get; set; }
        public static string SKUMin { get; set; }
        public static string SKUMax { get; set; }
    }
}