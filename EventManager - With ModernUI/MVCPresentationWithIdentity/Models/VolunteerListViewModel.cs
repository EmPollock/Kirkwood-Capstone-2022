using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;

namespace MVCPresentationWithIdentity.Models
{
    public class VolunteerListViewModel
    {
        public IEnumerable<Volunteer> Volunteers { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}