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
    public class EventDateManager : IEventDateManager
    {
        IEventDateAccessor _eventDateAccessor = null;

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Default constructor for event date manager using the event date accessor
        /// </summary>
        public EventDateManager()
        {
            _eventDateAccessor = new EventDateAccessor();
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Constructor for event date manager passing an event date accessor for testing purposes
        /// 
        /// </summary>
        /// <param name="eventDateAccessor">Event Date accessor</param>
        public EventDateManager(IEventDateAccessor eventDateAccessor)
        {
            _eventDateAccessor = eventDateAccessor;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Calls insert event date
        /// </summary>
        /// <param name="eventDate"></param>
        /// <returns>True if added to database</returns>
        public bool CreateEventDate(EventDate eventDate)
        {
            bool result = false;

            if (eventDate.EventDateID == null)
            {
                throw new ApplicationException("Event date can not be empty.");
            }

            try
            {
                result = ( 1 == _eventDateAccessor.InsertEventDate(eventDate));
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
        /// Retrieves a list of event dates that are associated with an event
        /// </summary>
        /// <param name="eventID">The EventID</param>
        /// <returns>A list of EventDates</returns>
        public List<EventDate> RetrieveEventDatesByEventID(int eventID)
        {
            List<EventDate> eventDates = null;

            try
            {
                eventDates = _eventDateAccessor.SelectEventDatesByEventID(eventID);
                //if (eventDates.Count == 0)
                //{
                //    throw new ApplicationException("No dates found for this event.");
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return eventDates;
        }
    }
}
