using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface ISupplierManager
    {
        List<Supplier> RetrieveActiveSuppliers();
        List<Reviews> RetrieveSupplierReviewsBySupplierID(int supplierID);
        List<string> RetrieveSupplierTagsBySupplierID(int supplierID);
        List<string> RetrieveSupplierImagesBySupplierID(int supplierID);
    }
}
