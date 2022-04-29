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

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Manager for supplier availability for the next three months
        /// 
        /// </summary>
        /// <param name="supplierID">The id for the supplier</param>
        /// <returns>A list of dates that the supplier is available</returns>
        public List<DateTime> SupplierAvailabilityForNextThreeMonths(int supplierID)
        {
            List<DateTime> datesAvailable = new List<DateTime>();
            // green
            //datesAvailable.Add(new Availability());
            //datesAvailable.Add(new Availability());
            //datesAvailable.Add(new Availability());
            //datesAvailable.Add(new Availability());

            try
            {
                datesAvailable = _supplierAccessor.SelectSupplierAvailabilityForNextThreeMonths(supplierID);
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return datesAvailable;
        }

        /// Austin Timmerman
        /// Created: 2022/04/09
        /// 
        /// Description:
        /// Retrieve supplier availability by SupplierID 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>A list of AvailabilityVM objects</returns>
        public List<AvailabilityVM> RetrieveSupplierAvailabilityBySupplierID(int supplierID)
        {
            List<AvailabilityVM> availabilities = new List<AvailabilityVM>();

            try
            {
                availabilities = _supplierAccessor.SelectSupplierAvailabilityBySupplierID(supplierID);
            }
            catch (Exception)
            {

                throw;
            }

            return availabilities;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/09
        /// 
        /// Description:
        /// Retrieve supplier availability exceptions by SupplierID
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>A list of Availability objects</returns>
        public List<Availability> RetrieveSupplierAvailabilityExceptionBySupplierID(int supplierID)
        {
            List<Availability> availabilities = new List<Availability>();

            try
            {
                availabilities = _supplierAccessor.SelectSupplierAvailabilityExceptionBySupplierID(supplierID);
            }
            catch (Exception)
            {

                throw;
            }

            return availabilities;
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/04
        /// 
        /// Description:
        /// Retrieves a supplier from the supplier table.
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>A supplier with the given supplierId</returns>

        public Supplier RetrieveSupplierBySupplierID(int supplierID)
        {
            if (supplierID < 99999)
            {
                throw new ApplicationException("Supplier not found.");
            }
            Supplier supplier = null;
            try
            {
                supplier = _supplierAccessor.SelectSupplierBySupplierID(supplierID);
                if (supplier is null || supplier.Name.Length == 0)
                {
                    throw new ApplicationException("Supplier not found.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve supplier.");
            }

            return supplier;
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/04
        /// 
        /// Description:
        /// Inserts a new requested supplier
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns>A supplier with the given supplierId</returns>
        public int CreateSupplier(Supplier supplier)
        {
            if (supplier.Name.Length > 160 || supplier.Name.Length < 1) 
            {
                throw new ApplicationException("Name must be bewtween 1-160 characters.");
            }
            if (supplier.Description != null && supplier.Description.Length > 3000) 
            {
                throw new ApplicationException("Description cannot exceed 3000 characters");
            }
            if (supplier.Email.Length > 100 || supplier.Email.Length < 1) 
            {
                throw new ApplicationException("Email must be bewtween 1-100 characters.");
            }
            if (supplier.Phone.Length > 15 || supplier.Phone.Length < 1) 
            {
                throw new ApplicationException("Invalid phone number.");
            }
            if (supplier.Address1.Length > 100 || supplier.Address1.Length < 1) 
            {
                throw new ApplicationException("Invalid address.");
            }
            if (supplier.ZipCode.Length > 160 || supplier.ZipCode.Length < 1) 
            {
                throw new ApplicationException("Invalid zip code.");
            }
            int rows = 0;
            try
            {
                rows = _supplierAccessor.InsertSupplier(supplier);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Unable to create supplier. " + e.Message);
            }
            return rows;
        }
    }
}
