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
    }
}
