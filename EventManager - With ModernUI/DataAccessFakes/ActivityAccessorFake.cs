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

            _fakeActivites.Add(new ActivityVM()
            {
                ActivityID = 1000004,
                ActivityName = "Test Activity 5",
                ActivityDescription = "The description of activity 5",
                PublicActivity = true,
                StartTime = new DateTime(2022, 01, 03).AddHours(1),
                EndTime = new DateTime(2022, 01, 03).AddHours(3),
                SublocationID = 1000002,
                EventDateID = new DateTime(2022, 01, 01),
                EventID = 2
            });

            _fakeActivites.Add(new ActivityVM()
            {
                ActivityID = 1000005,
                ActivityName = "Test Activity 6",
                ActivityDescription = "The description of activity 6",
                PublicActivity = true,
                StartTime = new DateTime(2022, 01, 03).AddHours(1),
                EndTime = new DateTime(2022, 01, 03).AddHours(3),
                SublocationID = 1000001,
                EventDateID = new DateTime(2022, 01, 02),
                EventID = 2
            });
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// Inserts a fake Activity for testing
        /// 
        /// </summary>
        /// <param name="activity"></param>
        /// <returns>The number of rows affected, expected 1</returns>

        public int InsertActivity(Activity activity)
        {
            int rowsAffected = 0;

            try
            {
                _fakeActivites.Add(new ActivityVM()
                {
                    ActivityID = _fakeActivites[0].ActivityID + 1,
                    ActivityName = activity.ActivityName,
                    ActivityDescription = activity.ActivityDescription,
                    PublicActivity = activity.PublicActivity,
                    StartTime = activity.StartTime,
                    EndTime = activity.EndTime,
                    SublocationID = activity.SublocationID,
                    EventDateID = activity.EventDateID,
                    EventID = activity.EventID
                });
                rowsAffected = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rowsAffected;
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

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// Select fake Activities that belong to a specific event 
        ///  and event date for testing
        /// 
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="eventDateID"></param>
        /// <returns>A List of Activities for a date of an event</returns>
        public List<Activity> SelectActivitiesByEventIDAndEventDateID(int eventID, DateTime? eventDateID)
        {
            List<Activity> result = new List<Activity>();

            foreach (Activity activity in _fakeActivites)
            {
                if (activity.EventID == eventID && activity.EventDateID == eventDateID)
                {
                    result.Add(activity);
                }
            }

            return result;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/23
        /// 
        /// Description:
        /// Select fake Activities that have the specific sublocation ID passed to it
        /// 
        /// </summary>
        /// <param name="sublocationID"></param>
        /// <returns>A List of Activities</returns>
        public List<Activity> SelectActivitiesBySublocationID(int sublocationID)
        {
            List<Activity> activities = new List<Activity>();

            foreach(Activity activity in _fakeActivites)
            {
                if(activity.SublocationID == sublocationID)
                {
                    activities.Add(activity);
                }
            }

            return activities;
        }
    }
}
