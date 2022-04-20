using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    /// <summary>
    /// Kris Howell
    /// Created: 2022/02/03
    /// 
    /// Description:
    /// Data object for a location
    /// 
    /// </summary>
    public class Location
    {
        public int LocationID { get; set; }
        public int? UserID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Location Name")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Pricing Information")]
        public string PricingInfo { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Location Address")]
        public string Address1 { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Second Location Address")]
        public string Address2 { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "City of the Location")]
        public string City { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "State of the Location")]
        public string State { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Location Zip Code")]
        public string ZipCode { get; set; }
        public string ImagePath { get; set; }
        public int AverageRating { get; set; }
        public bool Active { get; set; }
    }
}
