using System;
using System.Collections;
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
        public int? LocationID { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventCreatedDate { get; set; }
        public decimal TotalBudget { get; set; }
        public bool Active { get; set; }

        public Event()
        {
            Active = true;
        }
    }

    /// <summary>
    /// Derrick Nagy
    /// Created: 2022/02/06
    /// 
    /// Description:
    /// Create the view model for event
    /// 
    /// </summary>
    public class EventVM : Event
    {
        public List<EventDate> EventDates { get; set; }

        public EventVM()
        {
            EventDates = new List<EventDate>();
        }
    }

}
