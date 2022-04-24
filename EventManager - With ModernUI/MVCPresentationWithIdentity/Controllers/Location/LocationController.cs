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
        ILocationManager _locationManager = null;
        IEventDateManager _eventDateManager = null;
        LocationScheduleViewModel _locationSchedule = new LocationScheduleViewModel();

        public int _pageSize = 10;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/04
        /// 
        /// Description:
        /// Constructor that sets the _locationManager
        /// </summary>
        /// <param name="locationManager"></param>
        public LocationController(ILocationManager locationManager, IEventDateManager eventDateManager)
        {
            _locationManager = locationManager;
            _eventDateManager = eventDateManager;
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
            List<Location> locations = new List<Location>();
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
        /// Austin Timmerman
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// For the View Location Schedule page
        /// </summary>
        /// <param name="page"></param>
        /// <returns>ActionResult</returns>
        public ActionResult ViewLocationSchedule(int locationID = 0)
        {
            List<EventDateVM> eventDates = new List<EventDateVM>();
            //eventDates = _eventDateManager.RetrieveEventDatesByLocationID(location.LocationID);
            //_location = location;
            if (locationID == 0)
            {
                return RedirectToAction("ViewLocations", "Location");
            }
            Location location = new Location();
            location = _locationManager.RetrieveLocationByLocationID(locationID);
            _locationSchedule.Location = location;
            GetAvailability(location.LocationID);
            return View("~/Views/Location/ViewLocationSchedule.cshtml", _locationSchedule);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// For getting events by the location id passed to it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JsonResult</returns>
        public JsonResult GetEvents(string id)
        {
            int locationID;
            try
            {
                locationID = int.Parse(id);
            }
            catch (Exception)
            {

                locationID = 0;
            }
            List<EventDateVM> eventDates;
            if (locationID == 0)
            {
                eventDates = new List<EventDateVM>();
            }
            else
            {
                eventDates = _eventDateManager.RetrieveEventDatesByLocationID(locationID);
            }
            return new JsonResult { Data = eventDates, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// For getting availability by the location id passed to it and sets it to the 
        /// _locationSchedule
        /// </summary>
        /// <param name="id"></param>
        public void GetAvailability(int id)
        {
            List<AvailabilityVM> availability;
            List<Availability> availabilityException;
            if (id == 0)
            {
                availability = new List<AvailabilityVM>();
            }
            else
            {
                availability = _locationManager.RetrieveLocationAvailabilityByLocationID(id);
            }
            if (id == 0)
            {
                availabilityException = new List<Availability>();
            }
            else
            {
                availabilityException = _locationManager.RetrieveLocationAvailabilityExceptionByLocationID(id);
            }

            _locationSchedule.Availability = availability;
            _locationSchedule.AvailabilityException = availabilityException;
		}
        
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/07
        /// 
        /// Description:
        /// Method that returns user to ViewLocationDetails View
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>ActionResult</returns>
        public ActionResult ViewLocationDetails(int locationID = 0)
        {
            if (locationID == 0)
            {
                return RedirectToAction("ViewLocations", "Location");
            }
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

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// Returns a location from the details page in edit mode
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>ActionResult, LocationEdit View</returns>
        [Authorize(Roles = "Administrator, Event Planner, Supplier")]
        public ActionResult LocationEdit(int locationID = 0)
        {
            Location location = null;
            if (locationID == 0)
            {
                return RedirectToAction("ViewLocations", "Location");
            }
            try
            {
                location = _locationManager.RetrieveLocationByLocationID(locationID);
                ViewBag.Title = "Edit " + location.Name;
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Location not found.");
            }
            return View(location);
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// Returns a location from the details page in edit mode
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>ActionResult, LocationEdit View</returns>
        [HttpPost]
        [Authorize(Roles = "Administrator, Event Planner, Supplier")]
        public ActionResult LocationEdit(Location location)
        {
            if (location.LocationID == 0)
            {
                return RedirectToAction("ViewLocations", "Location");
            }
            try
            {
                Location oldLocation = _locationManager.RetrieveLocationByLocationID(location.LocationID);
                if (_locationManager.UpdateLocationBioByLocationID(oldLocation, location) == 1)
                {
                    return RedirectToAction("ViewLocationDetails", new { locationID = location.LocationID });
                }
                else
                {
                    ModelState.AddModelError("", "Location could not be updated."); 
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Location could not be found.");
            }
            return View(location);
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/07
        /// 
        /// Description:
        /// Method that deactivates a location from the edit page
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>ActionResult, ViewLocations View</returns>
        [Authorize(Roles = "Administrator, Event Planner, Supplier")]
        public ActionResult DeleteLocation(int locationID = 0)
        {
            if (locationID == 0)
            {
                return RedirectToAction("ViewLocations", "Location");
            }
            if (ModelState.IsValid)
            {
                try
                {

                    _locationManager.DeactivateLocationByLocationID(locationID);
                    return RedirectToAction("ViewLocations", new { page = 1 });
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Location not found.");
                    return View();
                }
            }
            return View();

        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/17
        /// 
        /// Description:
        /// Method that returns the view for Creating a Location
        /// </summary>
        /// <returns>ActionResult</returns>
        [Authorize(Roles = "Administrator, Event Planner, Supplier")]
        public ActionResult CreateLocation()
        {
            return View();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/18
        /// 
        /// Description:
        /// Method that creates a new location listing
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult CreateLocation(Location location)
        {
            LocationDetailsViewModel locationDetails = new LocationDetailsViewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    if (_locationManager.CreateLocation(location.Name, location.Address1, location.City, location.State, location.ZipCode) == 1)
                    {
                        Location createdLocation = _locationManager.RetrieveLocationByNameAndAddress(location.Name, location.Address1);
                        _locationManager.UpdateLocationBioByLocationID(createdLocation, location);
                        return RedirectToAction("ViewLocationDetails", new { createdLocation.LocationID });
                    }
                    return View();
                }
                catch (Exception ex)
                {
                    if(ex.Message.Equals("The INSERT statement conflicted with the FOREIGN KEY constraint \"fk_ZIPCode_Location\". The conflict occurred in database \"tadpole_db\", table \"dbo.ZIP\", column 'ZIPCode'.\r\nThe statement has been terminated."))
                    {
                        ModelState.AddModelError("", "Invalid Zip Code");
                        return View();
                    }
                    ModelState.AddModelError("", "Location already exists.");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}