using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/01/26
    /// 
    /// Interface for the VolunteerAccessor
    /// </summary>
    public interface IVolunteerAccessor
    {

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Interface method for selecting all volunteers
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        List<Volunteer> SelectAllVolunteers();

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Interface method for selecting all volunteer reviews
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        List<Volunteer> SelectAllVolunteerReviews();
    }
}
