using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;
using LogicLayerInterfaces;
using DataObjects;
using WPFPresentation;
using DataAccessInterfaces;
using DataAccessFakes;
using MVCPresentationWithIdentity.Models;
using Microsoft.AspNet.Identity;

namespace MVCPresentationWithIdentity.Controllers
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/04/04
    /// 
    /// Interaction logic for VolunteerController
    /// </summary>
    public class VolunteerController : Controller
    {
        IVolunteerManager _volunteerManager;
        IVolunteerRequestManager _volunteerRequestManager;
        IUserManager _userManager;
        public int _pageSize = 10;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/04
        /// 
        /// Description:
        /// Constructor that sets the _volunteerManager
        /// </summary>
        /// <param name="locationManager"></param>
        public VolunteerController(IVolunteerManager volunteerManager, IVolunteerRequestManager volunteerRequestManager, IUserManager userManager)
        {
            _volunteerManager = volunteerManager;
            _volunteerRequestManager = volunteerRequestManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/04
        /// 
        /// Description:
        /// For the View Volunteers page
        /// </summary>
        /// <param name="page"></param>
        /// <returns>ActionResult</returns>
        public ActionResult ViewVolunteers(int page = 1)
        {
            List<Volunteer> volunteers = new List<Volunteer>();
            List<Volunteer> volunteerReviews = new List<Volunteer>();
            VolunteerListViewModel model = null;
            try
            {
                volunteers = _volunteerManager.RetrieveAllVolunteers();
                volunteerReviews = _volunteerManager.RetrieveAllVolunteerReviews();
            }
            catch (Exception ex)
            {
                return View();
            }

            for (int i = 0; i < volunteers.Count; i++)
            {
                for (int j = 0; j <= volunteerReviews.Count; j++)
                {
                    if (j == volunteerReviews.Count)
                    {
                        volunteers[i].Rating = 0;
                        break;
                    }

                    if (volunteers[i].VolunteerID == volunteerReviews[j].VolunteerID)
                    {
                        volunteers[i].Rating = volunteerReviews[j].Rating;
                        break;
                    }
                }

            }

            model = new VolunteerListViewModel
            {
                Volunteers = volunteers
                            .OrderBy(p => p.VolunteerID)
                            .Skip((page - 1) * _pageSize)
                            .Take(_pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = volunteers.Count()
                }
            };

            return View(model);
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/03/30
        /// 
        /// Description:
        ///     Sends user to a list of their incoming volunteer requests if they are logged in.
        /// </summary>
        /// 
        /// <returns>ActionResult</returns>
        [Authorize]
        public ActionResult ViewRequests()
        {          
            IEnumerable<VolunteerRequestViewModel> requestViewModels;
            string currentUserName = User.Identity.GetUserName();

            try
            {                
                int userID = _userManager.RetrieveUserByEmail(currentUserName).UserID;
                Volunteer volunteer = _volunteerManager.RetrieveVolunteerByUserID(userID);
                requestViewModels = _volunteerRequestManager.RetrieveAllRequestsForVolunteerByVolunteerID(volunteer.VolunteerID);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                requestViewModels = new List<VolunteerRequestViewModel>();
            }

            return View(requestViewModels);
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/03/31
        /// 
        /// Description:
        ///     Updates the volunteer approval for a volunteer request if they are logged in and their 
        ///         volunteerID matches the incoming volunteerID
        /// </summary>
        /// 
        /// <param name="id">requestID of the request to be updated</param>
        /// <param name="approve">new volunteer approval value</param>
        /// <param name="volunteerID">the volunteer's id</param>
        /// <returns>ActionResult</returns>
        public ActionResult Approve(int id, bool approve, int volunteerID)
        {
            string currentUserName = User.Identity.GetUserName();
            IEnumerable<VolunteerRequestViewModel> requestViewModels;
            try
            {
                int userID = _userManager.RetrieveUserByEmail(currentUserName).UserID;
                Volunteer currentVolunteer = _volunteerManager.RetrieveVolunteerByUserID(userID);
                VolunteerRequestViewModel oldRequest = _volunteerRequestManager.RetrieveRequestByRequestID(id);
                VolunteerRequestViewModel newRequest = _volunteerRequestManager.RetrieveRequestByRequestID(id);
                if (currentVolunteer.VolunteerID == volunteerID && oldRequest.VolunteerID == volunteerID)
                {                    
                    newRequest.VolunteerResponse = approve;

                    bool success = _volunteerRequestManager.EditVolunteerRequest(oldRequest, newRequest);
                }
                requestViewModels = _volunteerRequestManager.RetrieveAllRequestsForVolunteerByVolunteerID(currentVolunteer.VolunteerID);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                requestViewModels = new List<VolunteerRequestViewModel>();
            }
            return View("ViewRequests", requestViewModels);
        }
    }
}   