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
        int InsertEvent(string eventName, string eventDescription);
        List<Event> SelectActiveEvents();
        Event SelectEventByEventNameAndDescription(string eventName, string eventDescription);
    }
}



