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
        /// Emma Pollock
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Retrieves a list of Activity View Models that are associated with an event
        /// </summary>
        /// <param name="eventID">The EventID</param>
        /// <returns>A list of ActivityVMs</returns>
        public List<ActivityVM> RetrieveActivitiesByEventID(int eventID)
        {
            List<ActivityVM> result = new List<ActivityVM>();
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
        /// Austin Timmerman
        /// Created: 2022/02/23
        /// 
        /// Description:
        /// Retrieves a list of Activities that match the sublocationID parameter
        /// </summary>
        /// <param name="sublocationID">The EventID</param>
        /// <returns>A list of Activities</returns>
        public List<Activity> RetrieveActivitiesBySublocationID(int sublocationID)
        {
            List<Activity> activities = new List<Activity>();

            try
            {
                activities = _activityAccessor.SelectActivitiesBySublocationID(sublocationID);
            }
            catch (Exception)
            {

                throw;
            }

            return activities;
        }
    }
}
