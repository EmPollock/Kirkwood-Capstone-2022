using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;
using LogicLayerInterfaces;
using DataAccessLayer;

namespace LogicLayer
{
    public class SupplierManager : ISupplierManager
    {
        ISupplierAccessor _supplierAccessor = null;

        /// <summary>
        /// Kris Howell
        /// Created: 2022/01/27
        /// 
        /// Description:
        /// Default constructor for supplier manager using the live supplier accessor
        /// </summary>
        public SupplierManager()
        {
            _supplierAccessor = new SupplierAccessor();
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/01/27
        /// 
        /// Description:
        /// Constructor for supplier manager using a given supplier accessor
        /// </summary>
        /// <param name="supplierAccessor"></param>
        public SupplierManager(ISupplierAccessor supplierAccessor)
        {
            _supplierAccessor = supplierAccessor;
        }


        /// <summary>
        /// Kris Howell
        /// Created: 2022/01/27
        /// 
        /// Description:
        /// Retrieves all active suppliers from database

        /// </summary>
        /// <returns>List of all active suppliers</returns>
        public List<Supplier> RetrieveActiveSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();

            try
            {
                suppliers = _supplierAccessor.SelectActiveSuppliers();
            }
            catch (Exception)
            {
                throw;
            }

            return suppliers;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// Function to take a supplier ID and get a list of images
        /// </summary>
        /// <param name="supplierID">Supplier ID to use to search for images</param>
        /// <returns>A list of images for the supplier ID</returns>
        public List<string> RetrieveSupplierImagesBySupplierID(int supplierID)
        {
            List<string> result = new List<string>();
            try
            {
                result = _supplierAccessor.SelectSupplierImagesBySupplierID(supplierID);
            } catch(Exception ex) 
            {
                throw new ApplicationException("Failed to retrieve supplier images", ex);
            }
            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// Function to take a supplier ID and get a list of reviews
        /// </summary>
        /// <param name="supplierID">Supplier ID to use to search for reviews</param>
        /// <returns>A list of reviews for the supplier ID</returns>
        public List<Reviews> RetrieveSupplierReviewsBySupplierID(int supplierID)
        {
            List<Reviews> result = new List<Reviews>();
            try
            {
                result = _supplierAccessor.SelectSupplierReviewsBySupplierID(supplierID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve supplier reviews", ex);
            }
            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// Function to take a supplier ID and get a list of tags
        /// </summary>
        /// <param name="supplierID">Supplier ID to use to search for tags</param>
        /// <returns>A list of tags for the supplier ID</returns>
        public List<string> RetrieveSupplierTagsBySupplierID(int supplierID)
        {
            List<string> result = new List<string>();
            try
            {
                result = _supplierAccessor.SelectSupplierTagsBySupplierID(supplierID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve supplier tags", ex);
            }
            return result;
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Retrieve supplier availability on a given date by SupplierID 
        /// First tries to get any availability exceptions for the given date.
        /// If it fails to find any, then it retrieves the regular weekly availability.
        /// </summary>
        /// <param name="supplierID"></param>
        /// <param name="date"></param>
        /// <returns>A list of Availability objects</returns>
        public List<Availability> RetrieveSupplierAvailabilityBySupplierIDAndDate(int supplierID, DateTime date)
        {
            List<Availability> supplierAvailabilities = new List<Availability>();

            try
            {
                supplierAvailabilities = _supplierAccessor.SelectSupplierAvailabilityExceptionBySupplierIDAndDate(supplierID, date);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve supplier availability exceptions", ex);
            }

            // if failed to find any exceptions, get regular weekly availability
            if (supplierAvailabilities.Count == 0)
            {
                try
                {
                    supplierAvailabilities = _supplierAccessor.SelectSupplierAvailabilityBySupplierIDAndDate(supplierID, date);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Failed to retrieve supplier availability", ex);
                }
            }

            return supplierAvailabilities;
        }
    }
}
