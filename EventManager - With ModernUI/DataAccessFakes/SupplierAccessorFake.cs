using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class SupplierAccessorFake : ISupplierAccessor
    {
        private List<Supplier> _fakeSuppliers = new List<Supplier>();

        /// <summary>
        /// Kris Howell
        /// Created: 2022/01/27
        /// 
        /// Description:
        /// Constructor to populate _fakeSuppliers with dummy values for testing purposes
        /// 
        /// </summary>
        public SupplierAccessorFake()
        {
            _fakeSuppliers.Add(new Supplier()
            {
                SupplierID = 100000,
                UserID = 100000,
                Name = "Test Supplier 1",
                Description = "Description of Test Supplier 1 goes here.",
                Phone = "111-111-1111",
                Email = "testSupplier1@suppliers.com",
                TypeID = "Vendor",
                Address1 = "Test Supplier 1 Street",
                Address2 = "Apt 1",
                Tags = new List<string>()
                {
                    "Test Tag 1",
                    "Test Tag 2"
                },
                Active = true
            });

            _fakeSuppliers.Add(new Supplier()
            {
                SupplierID = 100001,
                UserID = 100000,
                Name = "Test Supplier 2",
                Description = "Description of Test Supplier 2 goes here.",
                Phone = "222-222-2222",
                Email = "testSupplier2@suppliers.com",
                TypeID = "Vendor",
                Address1 = "Test Supplier 2 Street",
                Address2 = "Apt 2",
                Tags = new List<string>()
                {
                    "Test Tag 1",
                    "Test Tag 2"
                },
                Active = true
            });

            _fakeSuppliers.Add(new Supplier()
            {
                SupplierID = 100002,
                UserID = 100000,
                Name = "Test Supplier 3",
                Description = "Description of Test Supplier 3 goes here.",
                Phone = "333-333-3333",
                Email = "testSupplier3@suppliers.com",
                TypeID = "Vendor",
                Address1 = "Test Supplier 3 Street",
                Address2 = "Apt 3",
                Tags = new List<string>()
                {
                    "Test Tag 1",
                    "Test Tag 2"
                },
                Active = true
            });

            _fakeSuppliers.Add(new Supplier()
            {
                SupplierID = 100003,
                UserID = 100000,
                Name = "Test Supplier 4",
                Description = "Description of Test Supplier 4 goes here.",
                Phone = "444-444-4444",
                Email = "testSupplier4@suppliers.com",
                TypeID = "Vendor",
                Address1 = "Test Supplier 4 Street",
                Address2 = "Apt 4",
                Tags = new List<string>()
                {
                    "Test Tag 1",
                    "Test Tag 2"
                },
                Active = true
            });
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/01/27
        /// 
        /// Description:
        /// Returns all active suppliers in fake supplier list
        /// </summary>
        /// 
        /// <returns>List of active suppliers</returns>
        public List<Supplier> SelectActiveSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();

            foreach (Supplier fakeSupplier in _fakeSuppliers)
            {
                if (fakeSupplier.Active)
                {
                    suppliers.Add(fakeSupplier);
                }
            }

            return suppliers;
        }
    }
}
