﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayerInterfaces;
using DataObjects;
using Microsoft.AspNet.Identity;
using MVCPresentationWithIdentity.Models;
using System.Net;

namespace MVCPresentationWithIdentity.Controllers
{
    //[Authorize]
    public class EventController : Controller
    {
        private IEventManager _eventManager;
        private IUserManager _userManager;
        private IActivityManager _activityManager;

        private List<EventVM> eventList = null;
        public EventController(IEventManager eventManager, IUserManager userManger, IActivityManager activityManager)
        {
            _eventManager = eventManager;
            _userManager = userManger;
            _activityManager = activityManager;
           
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/14
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        // GET: Event
        public PartialViewResult EventNavBar(int eventID)
        {
            return PartialView(eventID);
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
        /// 2022/04/06
        /// 
        /// Description:
        /// Controller that returns a list that includes the search word
        /// in the event name, description, or in the location name, city, or state
        /// 
        /// </summary>
        /// <param name="search">Search criteria</param>
        /// <returns>EventList View</returns>
        [HttpPost]
        public ActionResult EventList(string search)
        {

            if (search.Length > 50)
            {
                TempData["errorMessage"] = "Search criteria too long. Please shorten.";
                eventList = new List<EventVM>();
            }
            else
            {
                try
                {
                    eventList = _eventManager.RetrieveEventListForSearch(search);
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

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/04/10
        /// 
        /// Description:
        /// ActionResult for clicking on Details button on
        /// view all activities page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ItineraryDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int activityID = (int)id;

            ActivityVM activity = _activityManager.RetrieveActivityVMByActivityID(activityID);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

    }

}