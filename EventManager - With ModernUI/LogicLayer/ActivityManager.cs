using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessInterfaces;
using DataAccessFakes;
using DataAccessLayer;

namespace LogicLayer
{
    public class ActivityManager : IActivityManager
    {
        IActivityAccessor _activityAccessor = null;
        IEventDateAccessor _eventDateAccessor = null;
        ISublocationAccessor _sublocationAccessor = null;
        IActivityResultAccessor _activityResultAccessor = null;

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Default constructor for ActivityManager using real data accessor
        /// </summary>
        public ActivityManager()
        {
            _activityAccessor = new ActivityAccessor();
            _eventDateAccessor = new EventDateAccessor();
            _sublocationAccessor = new SublocationAccessor();
            _activityResultAccessor = new ActivityResultAccessor();
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Constructor for ActivityManger passing data accessors for testing purposes
        /// 
        /// </summary>
        /// <param name="activityAccessor"></param>
        /// <param name="eventDateAccessor"></param>
        /// <param name="sublocationAccessor"></param>
        /// <param name="activityResultAccessor"></param>

        public ActivityManager(IActivityAccessor activityAccessor, IEventDateAccessor eventDateAccessor, 
            ISublocationAccessor sublocationAccessor, IActivityResultAccessor activityResultAccessor)
        {
            _activityAccessor = activityAccessor;
            _eventDateAccessor = eventDateAccessor;
            _sublocationAccessor = sublocationAccessor;
            _activityResultAccessor = activityResultAccessor;
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/25
        /// 
        /// Description:
        /// Retrieves a list of all public Activities in View Model 
        /// </summary>
        /// <returns>A list of ActivityVMs</returns>
        public List<ActivityVM> RetreiveActivitiesPastAndUpcomingDates()
        {
            List<ActivityVM> result = new List<ActivityVM>();

            try
            {
                result = _activityAccessor.SelectActivitiesPastAndUpcomingDates();
            }
            catch (Exception ex) { throw; }

            return result;

        }
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/25
        /// 
        /// Description:
        /// Retrieves a list of all user Activities in View Model 
        /// </summary>
        /// <returns>A list of ActivityVMs</returns>
        public List<ActivityVM> RetreiveUserActivitiesPastAndUpcomingDates(int userID)
        {
            List<ActivityVM> result = new List<ActivityVM>();
            try
            {
                result = _activityAccessor.SelectUserActivitiesPastAndUpcomingDates(userID);
            }
            catch (Exception ex) { throw ex; }

            return result;
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Retrieves a list of Activity View Models that are associated with an event
        /// </summary>
        /// <param name="eventID">The EventID</param>
        /// <returns>A list of ActivityVMs</returns>
        /// /// <summary>
        /// Logan Baccam
        /// Updated: 2022/02/25
        /// Description:
        /// Reverted changes
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns>A list of Activity objects</returns>
        public List<Activity> RetrieveActivitiesByEventID(int eventID)
        {
            List<Activity> result = new List<Activity>();
            try{
                List<Activity> activities =_activityAccessor.SelectActivitiesByEventID(eventID);

                foreach (Activity activity in activities)
                {
                    //set list of ActivityResults
                    List<ActivityResult> activityResults = _activityResultAccessor.SelectActivityResultsByActivityID(activity.ActivityID);

                    //set ActivitySublocation
                    Sublocation activitySublocation = _sublocationAccessor.SelectSublocationBySublocationID(activity.SublocationID);

                    //set EventDate
                    EventDate activityEventDate = _eventDateAccessor.SelectEventDateByEventDateIDAndEventID(activity.EventDateID, eventID);

                    result.Add(new ActivityVM()
                    {
                        ActivityID = activity.ActivityID,
                        ActivityName = activity.ActivityName,
                        ActivityDescription = activity.ActivityDescription,
                        PublicActivity = activity.PublicActivity,
                        StartTime = activity.StartTime,
                        EndTime = activity.EndTime,
                        ActivityImageName = activity.ActivityImageName,
                        SublocationID = activity.SublocationID,
                        EventDateID = activity.EventDateID,
                        EventID = activity.EventID,
                        ActivityResults = activityResults,
                        ActivitySublocation = activitySublocation,
                        EventDate = activityEventDate
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// Retrieves a list of Activity View Models that are associated with 
        ///     an event and event date
        /// </summary>
        /// <param name="eventID">The EventID</param>
        /// <param name="eventDateID">The EventDateID</param>
        /// <returns>A list of ActivityVMs</returns>
        public List<ActivityVM> RetrieveActivitiesByEventIDAndEventDateID(int eventID, DateTime? eventDateID)
        {
            List<ActivityVM> result = new List<ActivityVM>();
            try
            {
                List<Activity> activities = _activityAccessor.SelectActivitiesByEventIDAndEventDateID(eventID, eventDateID);

                foreach (Activity activity in activities)
                {
                    //set list of ActivityResults
                    List<ActivityResult> activityResults = _activityResultAccessor.SelectActivityResultsByActivityID(activity.ActivityID);

                    //set ActivitySublocation
                    Sublocation activitySublocation = _sublocationAccessor.SelectSublocationBySublocationID(activity.SublocationID);

                    //set EventDate
                    EventDate activityEventDate = _eventDateAccessor.SelectEventDateByEventDateIDAndEventID(activity.EventDateID, eventID);

                    result.Add(new ActivityVM()
                    {
                        ActivityID = activity.ActivityID,
                        ActivityName = activity.ActivityName,
                        ActivityDescription = activity.ActivityDescription,
                        PublicActivity = activity.PublicActivity,
                        StartTime = activity.StartTime,
                        EndTime = activity.EndTime,
                        ActivityImageName = activity.ActivityImageName,
                        SublocationID = activity.SublocationID,
                        EventDateID = activity.EventDateID,
                        EventID = activity.EventID,
                        ActivityResults = activityResults,
                        ActivitySublocation = activitySublocation,
                        EventDate = activityEventDate
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/14
        /// 
        /// Description:
        /// Retrieves a list of all Activities for an event in View Model 
        /// </summary>
        /// <returns>A list of ActivityVMs</returns>
        public List<ActivityVM> RetrieveActivitiesByEventIDForVM(int eventID)
        {
            List<ActivityVM> result = new List<ActivityVM>();
            try
            {
                result = _activityAccessor.SelectActivitiesByEventIDForVM(eventID);
            }
            catch (Exception ex) { throw ex; }

            return result;
        }
    }
}
