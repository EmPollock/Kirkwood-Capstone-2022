using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IEventDateManager
    {
        bool CreateEventDate(EventDate eventDate);
        List<EventDate> RetrieveEventDatesByEventID(int eventID);
        bool UpdateEventDate(EventDate oldEventDate, EventDate newEventDate);
        // just here until time to implement
        //int DeactivateEventDateByEventDateIDandEventID(DateTime eventDateID, int eventID);
        //int DeleteEventDateByEventDateIDandEventID(DateTime eventDateID, int eventID);
    }
}
