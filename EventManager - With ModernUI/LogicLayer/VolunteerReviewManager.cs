using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessInterfaces;
using DataAccessLayer;

namespace LogicLayer
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/03/10
    /// 
    /// Description:
    /// The VolunteerReviewManager class for all logic layer methods relating to Volunteer Reviews
    /// </summary>
    public class VolunteerReviewManager : IVolunteerReviewManager
    {
        IVolunteerReviewAccessor _volunteerReviewAccessor = null;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Default constructor which sets the _volunteerReviewAccessor to the live accessor
        /// </summary>
        public VolunteerReviewManager()
        {
            _volunteerReviewAccessor = new VolunteerReviewAccessor();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Custom constructor which sets the _volunteerReviewAccessor to the fake accessor
        /// </summary>
        public VolunteerReviewManager(IVolunteerReviewAccessor volunteerReviewAccessor)
        {
            _volunteerReviewAccessor = volunteerReviewAccessor;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Method which retrieves all volunteer reviews by the passed in volunteerID
        /// </summary>
        /// <param name="volunteerID"></param>
        /// <returns>List of Reviews objects</returns>
        public List<Reviews> RetrieveVolunteerReviewsByVolunteerID(int volunteerID)
        {
            List<Reviews> volunteerReviews = new List<Reviews>();

            try
            {
                volunteerReviews = _volunteerReviewAccessor.SelectVolunteerReviewsByVolunteerID(volunteerID);
            }
            catch (Exception)
            {

                throw;
            }

            return volunteerReviews;
        }
    }
}
