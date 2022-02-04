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
        int UpdateEventDateByEventDateIDAndEventID(DateTime eventDate, int eventID, DateTime oldStartDateTime, DateTime newStartDateTime, DateTime oldEndDateTime, DateTime newEndDateTime);
        int DeactivateEventDateByEventDateIDandEventID(DateTime eventDateID, int eventID);
        int DeleteEventDateByEventDateIDandEventID(DateTime eventDateID, int eventID);
    }
}
