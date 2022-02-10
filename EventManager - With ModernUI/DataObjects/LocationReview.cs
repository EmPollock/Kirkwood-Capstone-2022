using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/02/04
    /// 
    /// Description:
    /// The locationreview data object
    /// </summary>
    public class LocationReview
    {
        public int LocationID { get; set; }
        public int ReviewID { get; set; }
        public string FullName { get; set; }
        public string ReviewType { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Active { get; set; }
    }
}
