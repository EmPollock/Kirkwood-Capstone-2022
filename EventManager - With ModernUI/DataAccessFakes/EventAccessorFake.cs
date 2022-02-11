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
                Location = new Location()
                {
                    LocationID = 100000,
                    UserID = 100000,
                    Name = "Test Location 1",
                    Description = "Description of Test Location 1 goes here.",
                    PricingInfo = "Pricing information for renting Test Location 1 goes here.",
                    Phone = "111-111-1111",
                    Email = "testLocation1@locations.com",
                    Address1 = "Test Location 1 Street",
                    City = "Iowa City",
                    State = "Iowa",
                    ZipCode = "52240",
                    ImagePath = "http://imagehost.com/testlocation1.png",
                    Active = true
                },
                EventName = "Test Event 1",
                EventDescription = "A description of test event 1",
                EventCreatedDate = DateTime.Now,
                Active = true
            });

            _fakeEvents.Add(new Event()
            {
                EventID = 1000001,
                Location = new Location()
                {
                    LocationID = 100001,
                    UserID = 100000,
                    Name = "Test Location 2",
                    Description = "Description of Test Location 2 goes here.",
                    PricingInfo = "Pricing information for renting Test Location 2 goes here.",
                    Phone = "222-222-2222",
                    Email = "testLocation2@locations.com",
                    Address1 = "Test Location 2 Street",
                    Address2 = "Apt 2",
                    City = "Cedar Rapids",
                    State = "Iowa",
                    ZipCode = "52404",
                    ImagePath = "http://imagehost.com/testlocation2.png",
                    Active = true
                },
                EventName = "Test Event 2",
                EventDescription = "A description of test event 2",
                EventCreatedDate = DateTime.Now.AddMinutes(1),
                Active = true
            });

            _fakeEvents.Add(new Event()
            {
                EventID = 1000002,
                Location = new Location()
                {
                    LocationID = 100002,
                    UserID = 100000,
                    Name = "Test Location 3",
                    Description = "Description of Test Location 3 goes here.",
                    PricingInfo = "Pricing information for renting Test Location 3 goes here.",
                    Phone = "333-333-3333",
                    Email = "testLocation3@locations.com",
                    Address1 = "Test Location 3 Street",
                    Address2 = "Apt 33",
                    City = "Chicago",
                    State = "Illinois",
                    ZipCode = "60007",
                    ImagePath = "http://imagehost.com/testlocation3.png",
                    Active = true
                },
                EventName = "Test Event 3",
                EventDescription = "A description of test event 3",
                EventCreatedDate = DateTime.Now.AddMinutes(2),
                Active = true
            });


            _fakeEvents.Add(new Event()
            {
                EventID = 1000003,
                Location = new Location()
                {
                    LocationID = 100003,
                    UserID = 100000,
                    Name = "Test Location 4",
                    Description = "Description of Test Location 4 goes here.",
                    PricingInfo = "Pricing information for renting Test Location 4 goes here.",
                    Phone = "444-444-4444",
                    Email = "testLocation4@locations.com",
                    Address1 = "Test Location 4 Street",
                    Address2 = "Apt 44",
                    City = "New York City",
                    State = "New York",
                    ZipCode = "10036",
                    ImagePath = "http://imagehost.com/testlocation4.png",
                    Active = true
                },
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

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Select fake event for testing
        /// 
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="eventDescription"></param>
        /// <returns></returns>
        public Event SelectEventByEventNameAndDescription(string eventName, string eventDescription)
        {
            Event fakeEvent = null;

            if (_fakeEvents.Exists(e => (e.EventName == eventName) && (e.EventDescription == eventDescription)))
            {
                fakeEvent = _fakeEvents.First(e => (e.EventName == eventName) && (e.EventDescription == eventDescription));
            }

            return fakeEvent;
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/02
        /// 
        /// Description:
        /// Updates an event in fake data list
        /// 
        /// </summary>
        /// <returns>int number of records affected</returns>
        /// 
        public int UpdateEvent(Event oldEvent, Event newEvent)
        {
            int rowsAffected = 0;

            foreach (var fakeEvent in _fakeEvents)
            {
                if (fakeEvent.EventID == newEvent.EventID && fakeEvent.EventName == oldEvent.EventName
                    && fakeEvent.EventDescription == oldEvent.EventDescription
                    && fakeEvent.Active == oldEvent.Active)
                {
                    fakeEvent.EventName = newEvent.EventName;
                    fakeEvent.EventDescription = newEvent.EventDescription;
                    fakeEvent.Active = newEvent.Active;
                    rowsAffected++;
                }
            }

            return rowsAffected;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/09
        /// 
        /// Description:
        /// Updates the location of a fake event record.
        /// </summary>
        /// <param name="eventID">Event ID of the event</param>
        /// <param name="oldLocationID">ID of the old location</param>
        /// <param name="newLocationID">ID of the new location</param>
        /// <returns>int - rows affected</returns>
        public int UpdateEventLocationByEventID(int eventID, int? oldLocationID, int? newLocationID)
        {
            int rowsAffected = 0;
            foreach (var fakeEvent in _fakeEvents)
            {
                if (fakeEvent.EventID == eventID && fakeEvent.Location.LocationID == oldLocationID)
                {
                    fakeEvent.Location.LocationID = (int)newLocationID;
                    rowsAffected++;
                }
            }
            return rowsAffected;
        }
    }
}
