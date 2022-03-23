using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface ILocationManager
    {
        int CreateLocation(string locationName, string address, string locationCity, string locationState, string locationZipCode);
        Location RetrieveLocationByNameAndAddress(string locationName, string address);
        List<Location> RetrieveActiveLocations();
        Location RetrieveLocationByLocationID(int locationID);
        List<Reviews> RetrieveLocationReviews(int locationID);
        List<LocationImage> RetrieveLocationImagesByLocationID(int locationID);
        int DeactivateLocationByLocationID(int locationID);
        List<Availability> RetrieveLocationAvailabilityByLocationIDAndDate(int locationID, DateTime date);

    }
}
