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

namespace MVCPresentationWithIdentity.Controllers.Locations
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/04/04
    /// 
    /// Interaction logic for LocationController
    /// </summary>
    public class LocationController : Controller
    {
        ILocationManager _locationManager;
        public int _pageSize = 10;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/04
        /// 
        /// Description:
        /// Constructor that sets the _locationManager
        /// </summary>
        /// <param name="locationManager"></param>
        public LocationController(ILocationManager locationManager)
        {
            _locationManager = locationManager;
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/09
        /// 
        /// Description:
        /// Returns the navivagion bar for a selected
        /// locations pages
        /// 
        /// Christopher Repko
        /// Updated: 2022/04/11
        /// Added a necessary model field to the view
        /// 
        /// </summary>
        public PartialViewResult LocationNav(int locationId)
        {
            return PartialView(locationId);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/04
        /// 
        /// Description:
        /// For the View Locations page
        /// </summary>
        /// <param name="page"></param>
        /// <returns>ActionResult</returns>
        public ActionResult ViewLocations(int page = 1)
        {
            List<DataObjects.Location> locations = new List<DataObjects.Location>();
            List<Reviews> locationReviews = new List<Reviews>();
            LocationListViewModel model = null;
            try
            {
                locations = _locationManager.RetrieveActiveLocations();
            }
            catch (Exception)
            {
                return View();
            }

            model = new LocationListViewModel
            {
                Locations = locations
                            .OrderBy(p => p.LocationID)
                            .Skip((page - 1) * _pageSize)
                            .Take(_pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = locations.Count()
                }
            };

            return View(model);
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/07
        /// 
        /// Description:
        /// Method that returns user to ViewLocationDetails View
        /// </summary>
        /// <param name="locationID"
        /// <returns>ActionResult</returns>
        public ActionResult ViewLocationDetails(int locationID)
        {
            Location location = null;
            LocationDetailsViewModel model = null;
            List<Reviews> locationReviews = null;
            List<LocationImage> locationImages = null;
            List<string> locationTags = null;
            try
            {
                ViewBag.Title = "Location Details";
                location = _locationManager.RetrieveLocationByLocationID(locationID);
                locationReviews = _locationManager.RetrieveLocationReviews(locationID);
                locationTags = _locationManager.RetrieveTagsByLocationID(locationID);
                locationImages = _locationManager.RetrieveLocationImagesByLocationID(locationID);

                if (locationReviews.Count != 0)
                {
                    int avg = 0;
                    int total = 0;
                    foreach (Reviews review in locationReviews)
                    {
                        avg += review.Rating;
                        total++;
                    }
                    int sum = avg / total;
                    location.AverageRating = sum;
                }

                model = new LocationDetailsViewModel()
                {
                    Location = location,
                    LocationReviews = locationReviews,
                    LocationTags = locationTags,
                    LocationImages = locationImages
                };
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Location not found.");
            }
            return View(model);
        }
    }
}