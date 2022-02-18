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
            /// Logan Baccam
            /// Created: 2022/01/25
            /// 
            /// Description:
            /// Creates a Location
            /// </summary>
            /// <param name="locationName"></param>
            /// <param name="address"></param>
            /// <param name="locationCity"></param>
            /// <param name="locationState"></param>
            /// <param name="locationZip"></param>
            /// <returns>Number of rows added</returns>
            public int CreateLocation(string locationName, string address, string locationCity, string locationState, string locationZip)
            {
                int rowsAffected = 0;
                if (locationName == "" || locationName == null)
                {
                    throw new ApplicationException("Location name can not be empty.");
                }
                if (locationName.Length > 160 || locationName.Length < 1)
                {
                    throw new ApplicationException("Invalid location name. Location name must be between 1-160 characters.");
                }
                if (address == "" || address == null)
                {
                    throw new ApplicationException("Location address can not be empty.");
                }
                if (address.Length > 100 || address.Length < 1)
                {
                    throw new ApplicationException("Invalid address. Address must be between 1-100 characters.");
                }
                if (locationCity == "" || locationCity == null)
                {
                    throw new ApplicationException("City can not be empty.");
                }
                if (locationCity.Length > 100 || locationCity.Length < 1)
                {
                    throw new ApplicationException("Invalid City name. City name must be between 1-100 characters.");
                }
                if (locationState == "" || locationState == null)
                {
                    throw new ApplicationException("State can not be empty.");
                }
                if (locationZip.Length > 100 || locationZip.Length < 1)
                {
                    throw new ApplicationException("Invalid zip code. Zip code must be between 1-100 characters.");
                }
                if (locationZip.Length > 100 || locationZip.Length < 1)
                {
                    throw new ApplicationException("Invalid Zip code. Zip code must be between 1-100 characters.");
                }
                try
                {
                    rowsAffected = _locationAccessor.InsertLocation(locationName, address, locationCity, locationState, locationZip);
                }
                catch (Exception ex) { throw ex; }

                return rowsAffected;
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
        /// Logan Baccam
        /// Created: 2022/01/25
        /// 
        /// Description:
        /// returns a matching LocationVM object
        /// </summary>
        /// <param name="locationName"></param>
        /// <param name="address"></param>
        /// <returns>A matching location record</returns>
        public Location RetrieveLocationByNameAndAddress(string locationName, string address)
        {
            Location _matchingLocation = new Location();
            if (locationName == "" || locationName == null)
            {
                throw new ApplicationException("Location name can not be empty.");
            }
            if (locationName.Length > 160 || locationName.Length < 1)
            {
                throw new ApplicationException("Invalid location name. Location name must be between 1-160 characters.");
            }
            if (address == "" || address == null)
            {
                throw new ApplicationException("Location address can not be empty.");
            }
            if (address.Length > 100 || address.Length < 1)
            {
                throw new ApplicationException("Invalid location name. Location name must be between 1-100 characters.");
            }
            try
            {
                _matchingLocation = _locationAccessor.SelectLocationByLocationNameAndAddress(locationName, address);
            }
            catch (Exception ex) { throw ex; }

            return _matchingLocation;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Method to retrieve location images by its LocationID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>A list of LocationImage objects</returns>
        public List<LocationImage> RetrieveLocationImagesByLocationID(int locationID)
        {
            List<LocationImage> locationImages = new List<LocationImage>();

            try
            {
                locationImages = _locationAccessor.SelectLocationImagesByLocationID(locationID);
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
        /// <param name="locationID"></param>
        /// <returns>A list of LocationReview objects</returns>
        public List<Reviews> RetrieveLocationReviews(int locationID)
        {
            List<Reviews> locationReviews = new List<Reviews>();

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

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/10
        /// 
        /// Description:
        /// Method to retrieve location availability by its LocationID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>A list of LocationAvailability objects</returns>
        public List<LocationAvailability> RetrieveLocationAvailability(int locationID)
        {
            List<LocationAvailability> locationAvailabilities = new List<LocationAvailability>();

            try
            {
                locationAvailabilities = _locationAccessor.SelectLocationAvailability(locationID);
            }
            catch (Exception)
            {

                throw;
            }

            return locationAvailabilities;
        }
    }
}
