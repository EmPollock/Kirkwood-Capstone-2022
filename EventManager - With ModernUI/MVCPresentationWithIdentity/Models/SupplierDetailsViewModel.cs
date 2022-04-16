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
        public List<ServiceVM> Services { get; set; }
    }
}