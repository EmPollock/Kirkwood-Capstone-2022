using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface ILocationAccessor
    {
        List<Location> SelectActiveLocations();

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Interface method for select a location by its LocationID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <param name="locationID"></param>
        Location SelectLocationByLocationID(int locationID);

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/04
        /// 
        /// Description:
        /// Interface method for select a location reviews by its LocationID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <param name="locationID"></param>
        List<LocationReview> SelectLocationReviews(int locationID);

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// Interface method for select a location images by its LocationID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <param name="locationID"></param>
        List<LocationImage> SelectLocationImages(int locationID);
    }
}
