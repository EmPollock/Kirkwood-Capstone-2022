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
        public ActionResult ViewLocationSchedule(Location location)
        {
            List<EventDateVM> eventDates = new List<EventDateVM>();
            //eventDates = _eventDateManager.RetrieveEventDatesByLocationID(location.LocationID);
            //_location = location;

            if (location.LocationID == 0)
            {

                return RedirectToAction("ViewLocations", "Location");
            }

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
    }
}