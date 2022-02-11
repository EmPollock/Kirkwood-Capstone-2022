using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{

    /// <summary>
    /// Derrick Nagy
    /// Created: 2022/01/22
    /// 
    /// Description:
    /// Data object for an event
    /// 
    /// </summary>

    public class Event
    {
        public int EventID { get; set; }
        public Location Location { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventCreatedDate { get; set; }
        public bool Active { get; set; }
    }
}
