using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class LocationAvailability
    {
        public int AvailabilityID { get; set; }
        public int LocationID { get; set; }
        public DateTime AvailableDay { get; set; }
        public DateTime AvailableTimeStart { get; set; }
        public DateTime AvailableTimeEnd { get; set; }
    }
}
