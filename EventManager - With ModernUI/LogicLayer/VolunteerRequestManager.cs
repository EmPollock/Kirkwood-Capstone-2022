/// <summary>
/// Vinayak Deshpande
/// 2022/01/26
/// 
/// Volunteer Requests Manager
/// </summary>

using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class VolunteerRequestManager : IVolunteerRequestManager
    {
        private IVolunteerRequestAccessor _volunteerRequestAccessor;

        public VolunteerRequestManager()
        {
            _volunteerRequestAccessor = new VolunteerRequestAccessor();
        }

        public VolunteerRequestManager(IVolunteerRequestAccessor requestAccessor)
        {
            this._volunteerRequestAccessor = requestAccessor;
        }
        public List<VolunteerRequestViewModel> RetrieveVolunteerRequests(int eventID)
        {
            List<VolunteerRequestViewModel> requests = null;
            try
            {
                requests = _volunteerRequestAccessor.SelectVolunteerRequestsByEventID(eventID);
            }
            catch (Exception)
            {

                throw;
            }
            return requests;
        }
    }
}
