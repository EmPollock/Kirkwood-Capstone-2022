using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Kris Howell
    /// Created: 2022/01/27
    /// 
    /// Description:
    /// Data object for a supplier
    /// 
    /// </summary>
    public class Supplier
    {
        public int SupplierID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string TypeID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int AverageRating { get; set; }
        public List<string> Tags { get; set; }
        public bool Active { get; set; }
        public bool? Approved { get; set; }
    }
    /// <summary>
    /// Derrick Nagy
    /// Created: 2022/04/05
    /// 
    /// Description:
    /// Create SupplierVM with their availablity for three months
    /// </summary>
    public class SupplierVM : Supplier
    {
        public List<DateTime> ThreeMonthAvailability { get; set; }

    }
}
