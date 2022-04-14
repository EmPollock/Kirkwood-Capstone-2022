using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayerInterfaces;
using DataObjects;
using Microsoft.AspNet.Identity;
using MVCPresentationWithIdentity.Models;

namespace MVCPresentationWithIdentity.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private IEventManager _eventManager;
        private IUserManager _userManager;

        private List<EventVM> eventList = null;
        public EventController(IEventManager eventManager, IUserManager userManger)
        {
            _eventManager = eventManager;
            _userManager = userManger;
           
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Controller for default list of events
        /// </summary>
        /// <param name="eventList">List of Events</param>
        /// <returns>EventList View</returns>
        public ActionResult EventList(List<EventVM> eventList)
        {

            if (eventList == null)
            {
                try
                {
                    eventList = _eventManager.RetrieveEventListForUpcomingDates();
                }
                catch (Exception ex)
                {
                    TempData["errorMessage"] = ex.Message;
                }
            }
            
            return View(eventList);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Gets the list of past event dates
        /// </summary>
        /// <returns>EventList View</returns>
        public ActionResult AllPastEvents()
        {
            try
            {
                eventList = _eventManager.RetrieveEventListForPastDates();

            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
            }

            return View("EventList", eventList);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Gets the list of upcoming and past event dates
        /// </summary>
        /// <returns>EventList View</returns>
        public ActionResult AllEvents()
        {

            try
            {
                eventList = _eventManager.RetrieveEventListForUpcomingAndPastDates();

            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
            }

            return View("EventList", eventList);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Gets the list of upcoming event dates for a user
        /// </summary>
        /// <returns>EventList View</returns>
        public ActionResult UserFutureEvents()
        {

            string currentUserName = User.Identity.GetUserName();

            try
            {
                int userID = _userManager.RetrieveUserByEmail(currentUserName).UserID;
                  eventList = _eventManager.RetrieveEventListForUpcomingDatesForUser(userID);

                
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }

            return View("EventList", eventList);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Gets the list of past event dates for a user
        /// </summary>
        /// <returns>EventList View</returns>
        public ActionResult UserPastEvents()
        {

            string currentUserName = User.Identity.GetUserName();

            try
            {
                int userID = _userManager.RetrieveUserByEmail(currentUserName).UserID;
                eventList = _eventManager.RetrieveEventListForPastDatesForUser(userID);

            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
            }

            return View("EventList", eventList);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Gets the list of event dates for a user
        /// </summary>
        /// <returns>EventList View</returns>
        public ActionResult UserAllEvents()
        {
            string currentUserName = User.Identity.GetUserName();

            try
            {
                int userID = _userManager.RetrieveUserByEmail(currentUserName).UserID;
                eventList = _eventManager.RetrieveEventListForPastAndUpcomingDatesForUser(userID);
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
            }

            return View("EventList", eventList);
        }
    }

}