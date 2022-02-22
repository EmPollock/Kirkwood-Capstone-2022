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
        List<VolunteerRequestViewModel> _fakeRequests = new List<VolunteerRequestViewModel>();

        public VolunteerRequestAccessorFake()
        {
            this._fakeRequests.Add(new VolunteerRequestViewModel()
            {
                RequestID = 999999,
                VolunteerID = 999999,
                TaskID = 999999,
                VolunteerResponse = false,
                EventResponse = true,
                VolunteerName = "Abe Zed",
                TaskName = "Test Task 1"
            });

            this._fakeRequests.Add(new VolunteerRequestViewModel()
            {
                RequestID = 999998,
                VolunteerID = 999998,
                TaskID = 999999,
                VolunteerResponse = true,
                EventResponse = true,
                VolunteerName = "Bell Yotta",
                TaskName = "Test Task 1"
            });

            this._fakeRequests.Add(new VolunteerRequestViewModel()
            {
                RequestID = 999997,
                VolunteerID = 999997,
                TaskID = 999998,
                VolunteerResponse = true,
                EventResponse = null,
                VolunteerName = "Chaos Xylophone",
                TaskName = "Test Task 2"
            });

            this._fakeRequests.Add(new VolunteerRequestViewModel()
            {
                RequestID = 999996,
                VolunteerID = 999996,
                TaskID = 999998,
                VolunteerResponse = null,
                EventResponse = true,
                VolunteerName = "Digger Wiggum",
                TaskName = "Test Task 2"
            });

            this._fakeRequests.Add(new VolunteerRequestViewModel()
            {
                RequestID = 999995,
                VolunteerID = 999995,
                TaskID = 999998,
                VolunteerResponse = true,
                EventResponse = false,
                VolunteerName = "Elf Verdant",
                TaskName = "Test Task 2"
            });


        }

        public List<VolunteerRequestViewModel> SelectVolunteerRequestsByEventID(int EventID)
        {
            List<VolunteerRequestViewModel> requests = new List<VolunteerRequestViewModel>();

            try
            {
                requests = _fakeRequests;
            }
            catch (Exception)
            {
                throw;
            }

            return requests;
        }
    }
}
