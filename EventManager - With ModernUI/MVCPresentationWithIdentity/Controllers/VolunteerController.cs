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

namespace MVCPresentationWithIdentity.Controllers
{
    public class VolunteerController : Controller
    {
        IVolunteerManager _volunteerManager;
        public int _pageSize = 10;

        public VolunteerController(IVolunteerManager volunteerManager)
        {
            _volunteerManager = volunteerManager;
        }



        // GET: Volunteer
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
    }
}