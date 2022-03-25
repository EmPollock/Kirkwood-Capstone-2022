using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface ISupplierAccessor
    {
        List<Supplier> SelectActiveSuppliers();
        List<Reviews> SelectSupplierReviewsBySupplierID(int supplierID);
        List<string> SelectSupplierTagsBySupplierID(int supplierID);
        List<string> SelectSupplierImagesBySupplierID(int supplierID);
        List<Availability> SelectSupplierAvailabilityBySupplierIDAndDate(int supplierID, DateTime date);
        List<Availability> SelectSupplierAvailabilityExceptionBySupplierIDAndDate(int supplierID, DateTime date);
    }
}
