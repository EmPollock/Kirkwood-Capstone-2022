using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace DataObjects
{

    /// <summary>
    /// Derrick Nagy
    /// Created: 2022/01/22
    /// 
    /// Description:
    /// Data object for an event
    /// 
    /// Derrick Nagy
    /// Update: 2022/03/24
    /// 
    /// Description:
    /// Added display format
    /// </summary>
    public class Event
    {
        public int EventID { get; set; }
        public int? LocationID { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
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
    /// Derrick Nagy
    /// Update: 2022/03/24
    /// 
    /// Description:
    /// Added display format and location and users to event vm
    /// </summary>
    public class EventVM : Event
    {
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public List<EventDate> EventDates { get; set; }
        public Location Location { get; set; }
        public List<User> EventManagers { get; set; }

        public EventVM()
        {
            EventDates = new List<EventDate>();
            Location = new Location();
            EventManagers = new List<User>();
        }
    }

}
