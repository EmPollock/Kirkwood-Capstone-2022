using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Kris Howell
    /// Created: 2022/02/03
    /// 
    /// Description:
    /// Data object for a location
    /// 
    /// </summary>
    public class Location
    {
        public int LocationID { get; set; }
        public int? UserID { get; set; }
        public string LocationName { get; set; }
        public string LocationDescription { get; set; }
        public string LocationPricingText { get; set; }
        public string LocationPhone { get; set; }
        public string LocationEmail { get; set; }
        public string LocationAddress1 { get; set; }
        public string LocationAddress2 { get; set; }
        public string LocationCity { get; set; }
        public string LocationState { get; set; }
        public string LocationZipCode { get; set; }
        public string LocationImagePath { get; set; }
        public bool Active { get; set; }
    }
}
