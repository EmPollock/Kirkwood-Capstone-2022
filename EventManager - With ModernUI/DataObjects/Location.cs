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
    /// 
    /// Derrick Nagy
    /// Updated: 2022/04/17
    /// 
    /// Description:
    /// Added default constructor values
    /// 
    /// </summary>
    public class Location
    {
        public int LocationID { get; set; }
        public int? UserID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PricingInfo { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ImagePath { get; set; }
        public int AverageRating { get; set; }
        public bool Active { get; set; }

        public Location()
        {
            LocationID = 0;
            UserID = null;
            Name = null;
            Description = null;
            PricingInfo = null;
            Phone = null;
            Email = null;
            Address1 = null;
            Address2 = null;
            City = null;
            State = null;
            ZipCode = null;
            ImagePath = null;
            AverageRating =  0;
            Active = true;
        }
    }

    
}
