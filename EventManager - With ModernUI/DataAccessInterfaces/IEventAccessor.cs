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
        int InsertEventReturnsEventID(string eventName, string eventDescription, int userID);

        List<EventVM> SelectActiveEvents();
        int UpdateEvent(EventVM oldEvent, EventVM newEvent);
        EventVM SelectEventByEventNameAndDescription(string eventName, string eventDescription);
        
        List<EventVM> SelectEventsUpcomingDates();
        List<EventVM> SelectEventsUpcomingAndPastDates();
        List<EventVM> SelectEventsPastDates();

        List<EventVM> SelectUserEventsForUpcomingDates(int userID);
        List<EventVM> SelectUserEventsForPastDates(int userID);
        List<EventVM> SelectUserEventsForPastAndUpcomingDates(int userID);

        int UpdateEventLocationByEventID(int eventID, int? oldLocationID, int? newLocationID);

    }
}



