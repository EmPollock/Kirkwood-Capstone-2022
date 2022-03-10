using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/03/02
    /// 
    /// The class holding all service accessor fake methods and data
    /// </summary>
    public class ServiceAccessorFake : IServiceAccessor
    {
        List<Service> _fakeServices = new List<Service>();

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// The ServiceAccessorFake custom constructor
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>List of Service objects</returns>
        public ServiceAccessorFake()
        {
            _fakeServices.Add(new Service()
            {
                ServiceID = 100000,
                SupplierID = 100000,
                ServiceName = "Fake Service One",
                Price = 10.10m,
                Description = "The number one fakest service out there",
                ServiceImagePath = "f43faecc-5d0f-4b4a-ba47-4c1d3ce56912.jpg"
            });
            _fakeServices.Add(new Service()
            {
                ServiceID = 100001,
                SupplierID = 100001,
                ServiceName = "Fake Service Two",
                Price = 35.12m,
                Description = "The number two fakest service out there",
                ServiceImagePath = "f43faecc-5d0f-4b4a-ba47-4c1d3ce56912.jpg"
            });
            _fakeServices.Add(new Service()
            {
                ServiceID = 100002,
                SupplierID = 100000,
                ServiceName = "Fake Service Three",
                Price = 0.73m,
                Description = "The number three fakest service out there",
                ServiceImagePath = "7263a839-3428-49f2-b26f-875d3811ef85.jpg"
            });
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Method to select all services that match supplied supplierID
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>List of Service objects</returns>
        public List<Service> SelectServicesBySupplierID(int supplierID)
        {
            List<Service> services = new List<Service>();

            try
            {
                foreach(Service service in _fakeServices)
                {
                    if(service.SupplierID == supplierID)
                    {
                        services.Add(service);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return services;
        }
    }
}
