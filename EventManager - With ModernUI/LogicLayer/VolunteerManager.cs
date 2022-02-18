using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;
using LogicLayerInterfaces;
using DataAccessLayer;

namespace LogicLayer
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/01/26
    /// 
    /// The VolunteerManager class for all volunteer related methods
    /// </summary>
    public class VolunteerManager : IVolunteerManager
    {
        IVolunteerAccessor _volunteerAccessor = null;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Constructor for volunteer manager using the volunteer accessor
        /// </summary>
        public VolunteerManager()
        {
            _volunteerAccessor = new VolunteerAccessor();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Constructor that takes an IVolunteerAccessor and sets it to the _volunteerAccessor field. For passing test data for the manager.
        /// </summary>
        /// <param name="volunteerAccessor">The custom accessor being passed through</param>
        public VolunteerManager(IVolunteerAccessor volunteerAccessor)
        {
            _volunteerAccessor = volunteerAccessor;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Method to retrieve a list of all volunteer reviews
        /// </summary>
        /// <returns>A list of volunteer data object shells for the volunteer ID and rating</returns>
        public List<Volunteer> RetrieveAllVolunteerReviews()
        {
            List<Volunteer> volunteers = new List<Volunteer>();

            try
            {
                volunteers = _volunteerAccessor.SelectAllVolunteerReviews();
            }
            catch (Exception)
            {

                throw;
            }

            return volunteers;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Method used to retrieve a list of all volunteers to display on the Volunteers tab / page
        /// </summary>
        /// <returns>A list of volunteer data objects</returns>
        public List<Volunteer> RetrieveAllVolunteers()
        {
            List<Volunteer> volunteers = new List<Volunteer>();

            try
            {
                volunteers = _volunteerAccessor.SelectAllVolunteers();
            }
            catch (Exception)
            {
                throw;
            }

            return volunteers;
        }
    }
}
