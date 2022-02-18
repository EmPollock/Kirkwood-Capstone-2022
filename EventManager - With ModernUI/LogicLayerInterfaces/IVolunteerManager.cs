using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/01/26
    /// 
    /// Interface for the VolunteerManager
    /// </summary>
    public interface IVolunteerManager
    {
        List<Volunteer> RetrieveAllVolunteers();
        List<Volunteer> RetrieveAllVolunteerReviews();
    }
}
