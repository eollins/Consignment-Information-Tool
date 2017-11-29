using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMC_Consignment_API.Models
{
    public class ConsignmentInformation
    {
        public string SKUMin { get; set; }
        public string SKUMax { get; set; }
        public string ConsignmentName { get; set; }
        public string Total { get; set; }
        public int IsTest { get; set; }
    }
}