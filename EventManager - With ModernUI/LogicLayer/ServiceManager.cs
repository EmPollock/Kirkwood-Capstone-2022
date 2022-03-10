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
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/03/02
    /// 
    /// Class for handling Service manager methods
    /// </summary>
    public class ServiceManager : IServiceManager
    {
        IServiceAccessor _serviceAccessor = null;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Default constructor for service manager using the live service accessor
        /// </summary>
        public ServiceManager()
        {
            _serviceAccessor = new ServiceAccessor();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Constructor for service manager using a given service accessor
        /// </summary>
        /// <param name="serviceAccessor"></param>
        public ServiceManager(IServiceAccessor serviceAccessor)
        {
            _serviceAccessor = serviceAccessor;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Method to retrieve a list of services by supplierID
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>List of Service objects</returns>
        public List<Service> RetrieveServicesBySupplierID(int supplierID)
        {
            List<Service> services = new List<Service>();

            try
            {
                services = _serviceAccessor.SelectServicesBySupplierID(supplierID);
            }
            catch (Exception)
            {

                throw;
            }

            return services;
        }
    }
}
