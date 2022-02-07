using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IEventAccessor
    {
        int InsertEvent(string eventName, string eventDescription, int locationID);
        List<Event> SelectActiveEvents();
        int UpdateEvent(Event oldEvent, Event newEvent);
        Event SelectEventByEventNameAndDescription(string eventName, string eventDescription);
    }
}



