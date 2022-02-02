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
        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// The interface method for retrieving all volunteers
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        List<Volunteer> RetrieveAllVolunteers();

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// The interface method for retrieving all volunteer reviews
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        List<Volunteer> RetrieveAllVolunteerReviews();
    }
}
