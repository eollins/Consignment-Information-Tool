using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMC_Consignment_API.Models
{
    public class ItemData
    {
        public string ItemNumber { get; set; }
        public string ConsignmentID { get; set; }
        public string SKU { get; set; }
        public string IsTest { get; set; }
    }
}