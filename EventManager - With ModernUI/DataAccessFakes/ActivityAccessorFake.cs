using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    public class ActivityAccessorFake : IActivityAccessor
    {
        private List<ActivityVM> _fakeActivites = new List<ActivityVM>();

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Constructor that adds fake Activities to the fakeActivites list for test data
        /// 
        /// </summary>
        
        public ActivityAccessorFake()
        {
            _fakeActivites.Add(new ActivityVM()
            {
                ActivityID = 1000000,
                ActivityName = "Test Activity 1",
                ActivityDescription = "The description of activity 1",
                PublicActivity = true,
                StartTime = new DateTime(2022, 01, 01).AddHours(3),
                EndTime = new DateTime(2022, 01, 01).AddHours(4),
                SublocationID = 1000003,
                EventDateID = new DateTime(2022, 01, 01),
                EventID = 1
            });

            _fakeActivites.Add(new ActivityVM()
            {
                ActivityID = 1000001,
                ActivityName = "Test Activity 2",
                ActivityDescription = "The description of activity 2",
                PublicActivity = true,
                StartTime = new DateTime(2022, 01, 01).AddHours(1),
                EndTime = new DateTime(2022, 01, 01).AddHours(3),
                SublocationID = 1000004,
                EventDateID = new DateTime(2022, 01, 01),
                EventID = 1
            });

            _fakeActivites.Add(new ActivityVM()
            {
                ActivityID = 1000002,
                ActivityName = "Test Activity 3",
                ActivityDescription = "The description of activity 3",
                PublicActivity = true,
                StartTime = new DateTime(2022, 06, 01).AddHours(1),
                EndTime = new DateTime(2022, 02, 01).AddHours(3),
                SublocationID = 1000002,
                EventDateID = new DateTime(2022, 01, 02),
                EventID = 3
            });

            _fakeActivites.Add(new ActivityVM()
            {
                ActivityID = 1000003,
                ActivityName = "Test Activity 4",
                ActivityDescription = "The description of activity 4",
                PublicActivity = true,
                StartTime = new DateTime(2022, 01, 03).AddHours(1),
                EndTime = new DateTime(2022, 01, 03).AddHours(3),
                SublocationID = 1000001,
                EventDateID = new DateTime(2022, 01, 01),
                EventID = 2
            });
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Select fake Activities that belong to a specific event for testing
        /// 
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns>A List of Activities for an Event</returns>
        public List<Activity> SelectActivitiesByEventID(int eventID)
        {
            List<Activity> result = new List<Activity>();

            foreach (Activity activity in _fakeActivites)
            {
                if(activity.EventID == eventID)
                {
                    result.Add(activity);
                }
            }
            if(result.Count == 0)
            {
                //Event has no activities
                throw new ArgumentException(); 
            }

            return result;
        }
    }
}
