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
    public class LocationManager : ILocationManager
    {
        ILocationAccessor _locationAccessor = null;

        /// <summary>
        /// Kris Howell
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Default constructor for location manager using the live location accessor
        /// </summary>
        public LocationManager()
        {
            _locationAccessor = new LocationAccessor();
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Constructor for location manager using a given location accessor
        /// </summary>
        /// <param name="locationAccessor"></param>
        public LocationManager(ILocationAccessor locationAccessor)
        {
            _locationAccessor = locationAccessor;
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Retrieves all active locations from database

        /// </summary>
        /// <returns>List of all active locations</returns>
        public List<Location> RetrieveActiveLocations()
        {
            List<Location> locations = new List<Location>();

            try
            {
                locations = _locationAccessor.SelectActiveLocations();
            }
            catch (Exception)
            {
                throw;
            }

            return locations;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Method to retrieve a location by its LocationID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <param name="locationID"></param>
        /// <returns>A Location object</returns>
        public Location RetrieveLocationByLocationID(int locationID)
        {
            Location location = new Location();

            try
            {
                location = _locationAccessor.SelectLocationByLocationID(locationID);
            }
            catch (Exception)
            {

                throw;
            }

            return location;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Method to retrieve location images by its LocationID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <param name="locationID"></param>
        /// <returns>A list of LocationImage objects</returns>
        public List<LocationImage> RetrieveLocationImages(int locationID)
        {
            List<LocationImage> locationImages = new List<LocationImage>();

            try
            {
                locationImages = _locationAccessor.SelectLocationImages(locationID);
            }
            catch (Exception)
            {

                throw;
            }

            return locationImages;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// Method to retrieve location reviews by its LocationID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <param name="locationID"></param>
        /// <returns>A list of LocationReview objects</returns>
        public List<LocationReview> RetrieveLocationReviews(int locationID)
        {
            List<LocationReview> locationReviews = new List<LocationReview>();

            try
            {
                locationReviews = _locationAccessor.SelectLocationReviews(locationID);
            }
            catch (Exception)
            {

                throw;
            }

            return locationReviews;
        }
    }
}
