using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IEventManager
    {
        int CreateEvent(string eventName, string eventDescription);
        List<Event> RetreieveActiveEvents();
        bool UpdateEvent(Event oldEvent, Event newEvent);
        Event RetrieveEventByEventNameAndDescription(string eventName, string eventDescription);
    }
}
