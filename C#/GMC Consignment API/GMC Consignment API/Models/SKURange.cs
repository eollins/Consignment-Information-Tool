using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMC_Consignment_API.Models
{
    public class SKURange
    {
        public int ConsignmentID { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
    }
}