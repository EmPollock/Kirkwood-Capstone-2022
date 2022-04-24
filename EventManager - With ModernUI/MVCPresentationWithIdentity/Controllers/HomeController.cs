using DataObjects;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentationWithIdentity.Controllers
{
    public class HomeController : Controller
    {
        private List<EventVM> _events;
        IEventManager _eventManager;
        IEventDateManager _eventDateManager;
        ILocationManager _locationManager;
        /// <summary>
        /// Vinayak Deshpande
        /// Updated: 2022/04/17
        /// 
        /// Description: Added Functionality for viewing all active events on the home page.
        /// </summary>
        /// <param name="eventManager"></param>
        /// <param name="eventDateManager"></param>
        /// <param name="locationManager"></param>
        public HomeController(IEventManager eventManager, IEventDateManager eventDateManager, ILocationManager locationManager)
        {
            _eventManager = eventManager;
            _eventDateManager = eventDateManager;
            _locationManager = locationManager;
            _events = _eventManager.RetreieveActiveEvents();
            foreach (var eventVM in _events)
            {
                List<EventDate> dates = _eventDateManager.RetrieveEventDatesByEventID(eventVM.EventID);
                eventVM.EventDates = dates != null ? dates : new List<EventDate>();
                if (eventVM.LocationID != null)
                {
                    eventVM.Location = _locationManager.RetrieveLocationByLocationID((int)eventVM.LocationID);
                }
            }
        }
        public ActionResult Index()
        {
            return View(_events);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}