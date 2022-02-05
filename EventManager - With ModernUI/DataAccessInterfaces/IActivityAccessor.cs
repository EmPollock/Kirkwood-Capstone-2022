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
        List<Activity> SelectActivitiesByEventID(int eventID);
    }
}
