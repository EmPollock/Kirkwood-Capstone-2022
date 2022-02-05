using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;


namespace DataAccessFakes
{
    public class EventDateAccessorFake : IEventDateAccessor
    {
        private List<EventDate> _fakeEventDate = new List<EventDate>();

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Constructor with fake data for testing being added to _fakeEventDates
        /// 
        /// </summary>
        public EventDateAccessorFake()
        {
            _fakeEventDate.Add(new EventDate()
            {
                EventDateID = new DateTime(2022, 01, 01),
                EventID = 1,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            }) ;

            _fakeEventDate.Add(new EventDate()
            {
                EventDateID = new DateTime(2022, 01, 02),
                EventID = 1,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            });

            _fakeEventDate.Add(new EventDate()
            {
                EventDateID = new DateTime(2022, 01, 03),
                EventID = 1,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            });


            _fakeEventDate.Add(new EventDate()
            {
                EventDateID = new DateTime(2022, 01, 01),
                EventID = 2,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            });

            _fakeEventDate.Add(new EventDate()
            {
                EventDateID = new DateTime(2022, 01, 01),
                EventID = 2,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            });

            _fakeEventDate.Add(new EventDate()
            {
                EventDateID = new DateTime(2022, 06, 01),
                EventID = 3,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            });
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Insert fake data about the date of the event for tesing
        /// 
        /// </summary>
        /// <param name="eventDate">An EventDate object</param>
        /// <returns>Number of rows added</returns>
        public int InsertEventDate(EventDate eventDate)
        {
            int rowsAffected = 0;

            foreach (EventDate fakeDate in _fakeEventDate)
            {
                if (fakeDate.EventDateID == eventDate.EventDateID && fakeDate.EventID == eventDate.EventID)
                {
                    throw new ApplicationException("This event already has this date added");
                }

            }

            _fakeEventDate.Add(eventDate);

            rowsAffected++;

            return rowsAffected;
        }


        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Return list of fake data about the date of the event for testing
        /// 
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns>Fake date list</returns>
        public List<EventDate> SelectEventDatesByEventID(int eventID)
        {
            List<EventDate> eventDates = new List<EventDate>();

            _fakeEventDate.ForEach(ed =>
           {
               if (ed.EventID == eventID)
               {
                   eventDates.Add(ed);
               }
           });

            return eventDates;
        }
    }
}
