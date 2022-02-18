using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;
using LogicLayerInterfaces;
using DataAccessLayer;

namespace LogicLayer
{
    public class EventManager : IEventManager
    {
        IEventAccessor _eventAccessor = null;

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Constructor for event manager using the event accessor
        /// </summary>
        public EventManager()
        {
            _eventAccessor = new EventAccessor();
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Constructor that takes an IEventAccessor and sets it to the _eventAccessor field. For passing test data for the manager.
        /// </summary>
        /// <param name="eventAccessor"></param>
        public EventManager(IEventAccessor eventAccessor)
        {
            _eventAccessor = eventAccessor;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Creates an event
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="eventDescription"></param>
        /// <returns>Number of rows added</returns>
        public int CreateEvent(string eventName, string eventDescription)
        {
            int rowsAffected = 0;


            if (eventName == "" || eventName == null)
            {
                throw new ApplicationException("Name can not be empty.");
            }
            if (eventName.Length >= 50)
            {
                throw new ApplicationException("Name can not be over 50 characters.");
            }

            if (eventDescription == "" || eventDescription == null)
            {
                throw new ApplicationException("Description can not empty.");
            }

            if (eventDescription.Length >= 1000)
            {
                throw new ApplicationException("Description can not over 1000 characters.");
            }


            try
            {
                rowsAffected = _eventAccessor.InsertEvent(eventName, eventDescription);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Retrieves Active Events from data source
        /// </summary>
        /// Derrick Nagy
        /// Updated: 2022/01/30
        /// 
        /// Description:
        /// Added variable "ex" so method throws ex
        /// <returns>List of active events</returns>
        public List<EventVM> RetreieveActiveEvents()
        {
            List<EventVM> events = new List<EventVM>();

            try
            {
                events = _eventAccessor.SelectActiveEvents(); // trying to get the list of events
        
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return events;
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/02
        /// 
        /// Description:
        /// Updates an Event record in data source
        /// </summary>
        /// <param name="oldEvent">The record previously stored</param>
        /// <param name="newEvent">The new record containing the updates to the old</param>
        /// <returns>True or false if one record was updated</returns>
        public bool UpdateEvent(EventVM oldEvent, EventVM newEvent)
        {
            bool result = false;

            if (newEvent.EventName == "" || newEvent.EventName == null)
            {
                throw new ApplicationException("Name can not be empty.");
            }
            if (newEvent.EventName.Length >= 50)
            {
                throw new ApplicationException("Name can not be over 50 characters.");
            }

            if (newEvent.EventDescription == "" || newEvent.EventDescription == null)
            {
                throw new ApplicationException("Description can not empty.");
            }

            if (newEvent.EventDescription.Length >= 1000)
            {
                throw new ApplicationException("Description can not over 1000 characters.");
            }

            try
            {
                result = 1 == _eventAccessor.UpdateEvent(oldEvent, newEvent);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Retrieve an event id based on the name and description
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="eventDescription"></param>
        /// <returns>EventID</returns>
        public EventVM RetrieveEventByEventNameAndDescription(string eventName, string eventDescription)
        {
            EventVM eventToGet = null;

            try
            {
                eventToGet = _eventAccessor.SelectEventByEventNameAndDescription(eventName, eventDescription);
                if (eventToGet == null)
                {
                    throw new ApplicationException("Could not find an event with that name and description.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return eventToGet;
        }


        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/06
        /// 
        /// Description:
        /// Retrieve a list of event view models
        /// </summary>
        /// <returns>List of event view models</returns>
        public List<EventVM> RetrieveEventListForUpcomingDates()
        {
            List<EventVM> eventVMs = new List<EventVM>();

            // Green
            //eventVMs.Add(new EventVM());
            //eventVMs.Add(new EventVM());
            //eventVMs.Add(new EventVM());

            try
            {
                eventVMs = _eventAccessor.SelectEventsUpcomingDates();
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return eventVMs;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Retrieve a list of event view models for both past and upcoming dates
        /// </summary>
        /// <returns>List of event view models</returns>
        public List<EventVM> RetrieveEventListForUpcomingAndPastDates()
        {

            List<EventVM> eventVMs = new List<EventVM>();

            // Green
            //eventVMs.Add(new EventVM());
            //eventVMs.Add(new EventVM());
            //eventVMs.Add(new EventVM());
            //eventVMs.Add(new EventVM());

            try
            {
                eventVMs = _eventAccessor.SelectEventsUpcomingAndPastDates();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return eventVMs;

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Retrieve a list of event view models for both past dates
        /// </summary>
        /// <returns>List of event view models</returns>
        public List<EventVM> RetrieveEventListForPastDates()
        {
            List<EventVM> eventVMs = new List<EventVM>();

            //Green
            //eventVMs.Add(new EventVM());

            try
            {
                eventVMs = _eventAccessor.SelectEventsPastDates();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return eventVMs;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Retrieve a list of event view models for future dates for a user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>List of event view models</returns>
        /// <summary>
        public List<EventVM> RetrieveEventListForUpcomingDatesForUser(int userID)
        {
            List<EventVM> eventVMs = new List<EventVM>();

            // Green
            //eventVMs.Add(new EventVM());
            //eventVMs.Add(new EventVM());
            //eventVMs.Add(new EventVM());

            try
            {
                eventVMs = _eventAccessor.SelectUserEventsForUpcomingDates(userID);
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return eventVMs;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Retrieve a list of event view models for past dates for a user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>List of event view models</returns>
        /// <summary>
        public List<EventVM> RetrieveEventListForPastDatesForUser(int userID)
        {
            List<EventVM> eventVMs = new List<EventVM>();

            // Green
            eventVMs.Add(new EventVM());

            try
            {
                eventVMs = _eventAccessor.SelectUserEventsForPastDates(userID);
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return eventVMs;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Retrieve a list of event view models for past and upcoming dates for a user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>List of event view models</returns>
        /// <summary>
        public List<EventVM> RetrieveEventListForPastAndUpcomingDatesForUser(int userID)
        {
            List<EventVM> eventVMs = new List<EventVM>();

            // Green
            //eventVMs.Add(new EventVM());
            //eventVMs.Add(new EventVM());
            //eventVMs.Add(new EventVM());


            try
            {
                eventVMs = _eventAccessor.SelectUserEventsForPastAndUpcomingDates(userID);
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return eventVMs;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/09
        /// 
        /// Description:
        /// Updates the locationID of an event
        /// </summary>
        /// <param name="eventID">EventID of the event to be updated</param>
        /// <param name="oldLocationID">The event's current locationID</param>
        /// <param name="newLocationID">The new location ID</param>
        /// <returns>bool - One row affected</returns>
        public bool UpdateEventLocationByEventID(int eventID, int? oldLocationID, int? newLocationID)
        {
            bool result = false;

            try
            {
                result = 1 == _eventAccessor.UpdateEventLocationByEventID(eventID, oldLocationID, newLocationID);
            } catch(Exception ex)
            {
                throw new ApplicationException("Failed to update event location", ex);
            }

            return result;

        }
        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/17
        /// 
        /// Description:
        /// Called the accessor that creates an event the event id
        /// </summary>
        /// <param name="eventName">The name of the event</param>
        /// <param name="eventDescription">The description of the event</param>
        /// <returns>Event ID as an int</returns>
        public int CreateEventReturnsEventID(string eventName, string eventDescription)
        {
            int eventID;
            // green
            // eventID = 1000000;
            throwExceptionsForBadEventNameOrDescription(eventName, eventDescription);
            try
            {
                eventID = _eventAccessor.InsertEventReturnsEventID(eventName, eventDescription);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return eventID;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/17
        /// 
        /// Description:
        /// Helper method for throwing exceptions if the event name or description are not correct
        /// </summary>
        /// <param name="eventName">The name of the event</param>
        /// <param name="eventDescription">Description of the event</param>
        private static void throwExceptionsForBadEventNameOrDescription(string eventName, string eventDescription)
        {
            if (eventName == "" || eventName == null)
            {
                throw new ApplicationException("Name can not be empty.");
            }
            if (eventName.Length >= 50)
            {
                throw new ApplicationException("Name can not be over 50 characters.");
            }

            if (eventDescription == "" || eventDescription == null)
            {
                throw new ApplicationException("Description can not empty.");
            }

            if (eventDescription.Length >= 1000)
            {
                throw new ApplicationException("Description can not over 1000 characters.");
            }
        }
    }
}
