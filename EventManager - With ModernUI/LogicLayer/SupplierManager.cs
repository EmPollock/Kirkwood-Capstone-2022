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
    }
}
