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
        public List<Event> RetreieveActiveEvents()
        {
            List<Event> events = new List<Event>();

            try
            {
                events = _eventAccessor.SelectActiveEvents();
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
        /// <returns>List of active events</returns>
        public bool UpdateEvent(Event oldEvent, Event newEvent)
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
            catch (Exception)
            {

                throw;
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
        public Event RetrieveEventByEventNameAndDescription(string eventName, string eventDescription)
        {
            Event eventToGet = null;

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
    }
}
