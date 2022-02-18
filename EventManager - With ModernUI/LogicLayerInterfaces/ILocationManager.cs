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
        int CreateLocation(string locationName, string address, string locationCity, string locationState, string locationZipCode);
        Location RetrieveLocationByNameAndAddress(string locationName, string address);
        List<Location> RetrieveActiveLocations();
        Location RetrieveLocationByLocationID(int locationID);
        List<LocationReview> RetrieveLocationReviews(int locationID);
        List<LocationImage> RetrieveLocationImagesByLocationID(int locationID);
        List<LocationAvailability> RetrieveLocationAvailability(int locationID);

    }
}
