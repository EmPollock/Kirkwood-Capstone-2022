using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IActivityManager
    {
        List<ActivityVM> RetrieveActivitiesByEventID(int eventID);
        List<ActivityVM> RetrieveActivitiesByEventIDAndEventDateID(int eventID, DateTime? eventDateID);
        List<Activity> RetrieveActivitiesBySublocationID(int sublocationID);
    }
}
