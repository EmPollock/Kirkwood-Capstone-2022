using DataObjects;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace MVCPresentationWithIdentity.Controllers.Event
{
    [Authorize]
    public class EventCreateController : Controller
    {
        private EventVM _eventVM;
        private IEventManager _eventManager;
        private IUserManager _userManager;

        public EventCreateController(IEventManager eventManager, IUserManager userManger)
        {
            _eventManager = eventManager;
            _userManager = userManger;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/21
        /// 
        /// Description:
        /// Controller for getting the form to create an event
        /// </summary>
        /// <returns>View for the form</returns>
        public ViewResult EventCreate()
        {

            return View();
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/21
        /// 
        /// Description:
        /// Controller for posting the form to create an event
        /// </summary>
        /// <param name="eventVM"></param>
        /// <returns>View to edit the rest of the form</returns>
        [HttpPost]
        public ActionResult CreateEvent(EventVM eventVM)
        {
            _eventVM = eventVM;

            if (ModelState.IsValid)
            {
                try
                {
                    string currentUserName = User.Identity.GetUserName();
                    int userID = _userManager.RetrieveUserByEmail(currentUserName).UserID;

                    _eventVM.EventID = _eventManager.CreateEventReturnsEventID(_eventVM.EventName, _eventVM.EventDescription, _eventVM.TotalBudget, userID);
                    TempData["eventID"] = _eventVM.EventID;
                }
                catch (Exception ex)
                {

                    TempData["errorMessage"] = ex.Message;
                    return View("EventCreate", _eventVM);

                }
            }

            
            // should return to details page
            // or page to add date, location, etc
            return RedirectToAction("UserFutureEvents", "Event");

        }

    }
}