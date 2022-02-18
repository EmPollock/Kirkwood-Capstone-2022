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
        Location SelectLocationByLocationID(int locationID);

        List<Reviews> SelectLocationReviews(int locationID);

        List<LocationImage> SelectLocationImagesByLocationID(int locationID);

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
        List<LocationAvailability> SelectLocationAvailability(int locationID);
    }
}
