using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Activity
    {
        public int ActivityID { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public bool PublicActivity { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ActivityImageName { get; set; }
        public int? SublocationID { get; set; }
        public DateTime EventDateID { get; set; }
        public int EventID { get; set; }
    }
}

