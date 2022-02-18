/// <summary>
/// Vinayak Deshpande
/// 2022/01/26
/// 
/// Volunteer Request Object Class
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataObjects
{
    public class VolunteerRequest
    {
        public int RequestID { get; set; }
        public int VolunteerID { get; set; }
        public int TaskID { get; set; }
        public bool? VolunteerResponse { get; set; }
        public bool? EventResponse { get; set; }

        public VolunteerRequest() 
        {

        }
        public VolunteerRequest(int requestID, int volunteerID, int taskID, bool? volunteerResponse, bool? eventResponse)
        {
            RequestID = requestID;
            VolunteerID = volunteerID;
            TaskID = taskID;
            VolunteerResponse = volunteerResponse;
            EventResponse = eventResponse;

        }
    }
}
