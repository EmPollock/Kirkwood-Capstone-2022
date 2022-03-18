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
        int InsertLocation(string locationName, string address, string locationCity, string locationState, string locationZipCode);
        Location SelectLocationByLocationNameAndAddress(string locationName, string address);
        List<Availability> SelectLocationAvailabilityByLocationIDAndDate(int locationID, DateTime date);
        List<Availability> SelectLocationAvailabilityExceptionByLocationIDAndDate(int locationID, DateTime date);
        int DeactivateLocationByLocationID(int locationID);
    }
}
