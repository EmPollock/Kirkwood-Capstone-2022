using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IEventDateAccessor
    {
        int InsertEventDate(EventDate eventDate);
        List<EventDate> SelectEventDatesByEventID(int eventID);
        int UpdateEventDateByEventDateIDAndEventID(DateTime eventDate, int eventID, DateTime oldStartDateTime, DateTime newStartDateTime, DateTime oldEndDateTime, DateTime newEndDateTime);
        int DeactivateEventDateByEventDateIDandEventID(DateTime eventDateID, int eventID);
        int DeleteEventDateByEventDateIDandEventID(DateTime eventDateID, int eventID);
    }
}
