using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMC_Consignment_API.Models
{
    public class ChangeItemNumber
    {
        public string ItemID { get; set; }
        public string NewItemNumber { get; set; }
        public string IsTest { get; set; }
    }
}