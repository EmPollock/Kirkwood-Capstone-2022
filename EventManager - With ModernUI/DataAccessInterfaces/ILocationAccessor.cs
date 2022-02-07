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

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/01/25
        /// 
        /// Description:
        /// Interface method for creating a new location

        /// </summary>
        /// <param name="locationName"></param>
        /// <param name="address"></param>
        /// <param name="locationCity"></param>
        /// <param name="locationState"></param>
        /// <param name="locationZip"></param>
        /// <returns>Number of rows added</returns>
        int InsertLocation(string locationName, string address, string locationCity, string locationState, string locationZipCode);
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/01/25
        /// 
        /// Description:
        /// Interface method for retreiving a new location

        /// </summary>
        /// <param name="locationName"></param>
        /// <param name="address"></param>
        Location SelectLocationByLocationNameAndAddress(string locationName, string address);
    }
}
