using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Vinayak Deshpande
/// Created 2022/01/28
/// 
/// Description
/// Accessor Fake for Volunteer Requests. Recreated after deletion.
/// </summary>
namespace DataAccessFakes
{
    public class VolunteerRequestAccessorFake : IVolunteerRequestAccessor
    {
        List<VolunteerRequest> _fakeRequests = new List<VolunteerRequest>();

        public VolunteerRequestAccessorFake() 
        {
            this._fakeRequests.Add(new VolunteerRequest()
            {
                RequestID = 999999,
                VolunteerID = 999999,
                TaskID = 999999,
                VolunteerResponse = false,
                EventResponse = true
            });

            this._fakeRequests.Add(new VolunteerRequest()
            {
                RequestID = 999998,
                VolunteerID = 999998,
                TaskID = 999999,
                VolunteerResponse = true,
                EventResponse = true
            });

            this._fakeRequests.Add(new VolunteerRequest()
            {
                RequestID = 999997,
                VolunteerID = 999999,
                TaskID = 999998,
                VolunteerResponse = true,
                EventResponse = true
            });

            this._fakeRequests.Add(new VolunteerRequest()
            {
                RequestID = 999996,
                VolunteerID = 999998,
                TaskID = 999998,
                VolunteerResponse = null,
                EventResponse = null
            });
        }

        public List<VolunteerRequest> SelectVolunteerRequestsByEventID(int EventID)
        {
            List<VolunteerRequest> requests = new List<VolunteerRequest>();

            try
            {
                requests = _fakeRequests;
            }
            catch(Exception)
            {
                throw;
            }

            return requests;
        }
    }
}
