using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IActivityAccessor
    {
        int InsertActivity(Activity activity);
        List<Activity> SelectActivitiesByEventID(int eventID);
        List<Activity> SelectActivitiesByEventIDAndEventDateID(int eventID, DateTime? eventDateID);
        List<Activity> SelectActivitiesBySublocationID(int sublocationID);
    }
}
