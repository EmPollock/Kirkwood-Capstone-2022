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
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// The interface method for creating a location 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <param name="locationName"></param>
        /// <param name="address"></param>
        /// <param name="locationCity"></param>
        /// <param name="locationState"></param>
        /// <param name="locationZipCode"></param>
        int CreateLocation(string locationName, string address, string locationCity, string locationState, string locationZipCode);

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// The interface method for retreiving a location by name and address
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <param name="locationName"></param>
        /// <param name="address"></param>
        Location RetrieveLocationByNameAndAddress(string locationName, string address);

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
