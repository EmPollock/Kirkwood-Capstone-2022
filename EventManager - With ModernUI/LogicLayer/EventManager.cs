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

            if (eventDescription == ""|| eventDescription == null)
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
        /// <returns>List of active events</returns>

        public List<Event> RetreieveActiveEvents()
        {
            List<Event> events = new List<Event>();

            try
            {
                events = _eventAccessor.SelectActiveEvents();
            }
            catch (Exception)
            {

                throw;
            }

            return events;
        }
    }
}
