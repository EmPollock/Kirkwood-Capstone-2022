using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class ActivityVM : Activity
    {
        public List<ActivityResult> ActivityResults { get; set; }
        public Sublocation ActivitySublocation { get; set; }
        public EventDate EventDate { get; set; }
        public int UserID { get; set; }
        public string SublocationName { get; set; }
    }
}
