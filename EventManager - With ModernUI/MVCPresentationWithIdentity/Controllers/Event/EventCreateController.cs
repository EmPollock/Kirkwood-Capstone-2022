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

        // GET: EventCreate
        public ViewResult EventCreate()
        {

            return View();
        }

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

                }
            }

            return View("EventCreate", _eventVM);
        }

    }
}