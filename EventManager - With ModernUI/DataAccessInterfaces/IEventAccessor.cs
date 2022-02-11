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
        int UpdateEvent(Event oldEvent, Event newEvent);
        Event SelectEventByEventNameAndDescription(string eventName, string eventDescription);
        
        List<EventVM> SelectEventsUpcomingDates();
        List<EventVM> SelectEventsUpcomingAndPastDates();
        List<EventVM> SelectEventsPastDates();

        List<EventVM> SelectUserEventsForUpcomingDates(int userID);
        List<EventVM> SelectUserEventsForPastDates(int userID);
        List<EventVM> SelectUserEventsForPastAndUpcomingDates(int userID);

        int UpdateEventLocationByEventID(int eventID, int? oldLocationID, int? newLocationID);

    }
}



