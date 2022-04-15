using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;

namespace MVCPresentationWithIdentity.Models
{
    public class SupplierDetailsViewModel
    {
        public Supplier Supplier { get; set; }
        public List<string> SupplierImages { get; set; }
        public List<string> SupplierTags { get; set; }
        public List<Reviews> SupplierReviews { get; set; }
    }

    public class EditSupplierModel : Supplier
    {
        public string NewName { get; set; }
        public string NewDescription { get; set; }
        public string NewPhone { get; set; }
        public string NewEmail { get; set; }
        public string NewTypeID { get; set; }
        public string NewAddress1 { get; set; }
        public string NewAddress2 { get; set; }
        public string NewCity { get; set; }
        public string NewState { get; set; }
        public string NewZipCode { get; set; }
        public List<string> NewTags { get; set; }
        public List<string> Images { get; set; }
        public List<string> NewImages { get; set; }
    }
}