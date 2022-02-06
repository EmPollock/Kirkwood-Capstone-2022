using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    /// <summary>
    /// Kris Howell
    /// Created: 2022/02/03
    /// 
    /// Description:
    /// Interface for handling Location manager class methods
    /// </summary>
    public interface ILocationManager
    {
        List<Location> RetrieveActiveLocations();

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// The interface method for retrieving a location by its LocationID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// /// <param name="locationID"></param>
        Location RetrieveLocationByLocationID(int locationID);

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/04
        /// 
        /// Description:
        /// The interface method for retrieving location reviews by its LocationID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// /// <param name="locationID"></param>
        List<LocationReview> RetrieveLocationReviews(int locationID);

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// The interface method for retrieving location images by its LocationID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// /// <param name="locationID"></param>
        List<LocationImage> RetrieveLocationImages(int locationID);
    }
}
