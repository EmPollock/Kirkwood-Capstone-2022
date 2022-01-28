using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;
namespace DataAccessFakes
{
    public class EventAccessorFake : IEventAccessor
    {
        private List<Event> _fakeEvents = new List<Event>();

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Constructor that adds fake events to _fakeEvents list for tesing purposes
        /// 
        /// </summary>
        public EventAccessorFake()
        {
            _fakeEvents.Add(new Event()
            {
                EventID = 1000000,
                EventName = "Test Event 1",
                EventDescription = "A description of test event 1",
                EventCreatedDate = DateTime.Now,
                Active = true
            });

            _fakeEvents.Add(new Event()
            {
                EventID = 1000001,
                EventName = "Test Event 2",
                EventDescription = "A description of test event 2",
                EventCreatedDate = DateTime.Now.AddMinutes(1),
                Active = true
            });

            _fakeEvents.Add(new Event()
            {
                EventID = 1000002,
                EventName = "Test Event 3",
                EventDescription = "A description of test event 3",
                EventCreatedDate = DateTime.Now.AddMinutes(2),
                Active = true
            });


            _fakeEvents.Add(new Event()
            {
                EventID = 1000003,
                EventName = "Test Event 4",
                EventDescription = "A description of test event 4",
                EventCreatedDate = DateTime.Now.AddMinutes(3),
                Active = true
            });
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Insert fake event for testing
        /// 
        /// </summary>
        /// <param name="eventName">Name of the event</param>
        /// <param name="eventDescription">Description fo the event</param>
        /// <returns>Number of rows inserted</returns>
        public int InsertEvent(string eventName, string eventDescription)
        {
            int rowsAffected = 0;
            int eventID = _fakeEvents.Last().EventID + 1;


            _fakeEvents.Add(new Event()
            {
                EventID = eventID,
                EventName = eventName,
                EventDescription = eventDescription,
                EventCreatedDate = DateTime.Now,
                Active = true
            });

            rowsAffected++;

            return rowsAffected;
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Selects all active events in fake data list
        /// 
        /// </summary>
        /// <returns>List of active events</returns>
        /// 
        public List<Event> SelectActiveEvents()
        {
            List<Event> events = new List<Event>();

            foreach (var fakeEvent in _fakeEvents) 
            { 
                if(fakeEvent.Active == true)
                {
                    events.Add(fakeEvent);
                }
            }

            return events;
        
        }
    }
}
