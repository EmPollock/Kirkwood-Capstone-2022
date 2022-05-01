using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using LogicLayer;
using LogicLayerInterfaces;
using DataObjects;
using WPFPresentation;
using DataAccessInterfaces;
using DataAccessFakes;
using MVCPresentationWithIdentity.Models;

namespace MVCPresentationWithIdentity.Controllers
{
    public class SupplierController : Controller
    {
        ISupplierManager _supplierManager;
        IActivityManager _activityManager = null;
        IServiceManager _serviceManager = null;
        IUserManager _userManager;
        IEmailProvider _emailProvider;
        SupplierScheduleViewModel _supplierSchedule = new SupplierScheduleViewModel();
        public int _pageSize = 10;


        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/02
        /// 
        /// Description:
        /// Default constructor for the Supplier controller
        /// </summary>
        public SupplierController(ISupplierManager supplierManager, IActivityManager activityManager, IServiceManager serviceManager, IUserManager userManager, IEmailProvider emailProvider)
        {
            _supplierManager = supplierManager;
            _activityManager = activityManager;
            _serviceManager = serviceManager;
            _userManager = userManager;
            _emailProvider = emailProvider;
        }

        public PartialViewResult SupplierNav(int eventId)
        {
            return PartialView(eventId);
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/02
        /// 
        /// Description:
        /// For the View Suppliers page
        /// </summary>
        /// <param name="page"></param>
        /// <returns>ActionResult</returns>
        public ActionResult ViewSuppliers(int page = 1)
        {
            List<Supplier> _suppliers = new List<Supplier>();
            List<Reviews> _supplierReviews = new List<Reviews>();
            SupplierListViewModel _model = null;

            if (_suppliers is null || _suppliers.Count == 0)
            {
                try
                {
                    _suppliers = _supplierManager.RetrieveActiveSuppliers();
                    foreach (Supplier supplier in _suppliers)
                    {
                        int avg = 0;
                        int total = 0;
                        _supplierReviews = _supplierManager.RetrieveSupplierReviewsBySupplierID(supplier.SupplierID);
                        if (_supplierReviews.Count != 0)
                        {
                            foreach (Reviews review in _supplierReviews)
                            {
                                avg += review.Rating;
                                total++;
                            }
                            int sum = avg / total;
                            supplier.AverageRating = sum;
                        }
                    }
                    _model = new SupplierListViewModel()
                    {
                        Suppliers = _suppliers.OrderBy(x => x.SupplierID)
                                              .Skip((page - 1) * _pageSize)
                                              .Take(_pageSize),
                        PagingInfo = new PagingInfo()
                        {
                            CurrentPage = page,
                            ItemsPerPage = _pageSize,
                            TotalItems = _suppliers.Count()
                        }
                    };
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return View(_model);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// For the View Supplier Schedule page
        /// 
        /// Logan Baccam
        /// Updated: 2022/04/14
        /// Description:
        /// Changed parameter from Supplier object
        /// to supplierID and changed Model type to
        /// SupplierDetailsViewModel
        /// 
        /// 
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult ViewSupplierSchedule(int supplierID = 0)
        {
            //Request.Params["supplier"];
            if (supplierID == 0)
            {

                return RedirectToAction("ViewSuppliers", "Supplier");
            }
            Supplier supplier = new Supplier();
            supplier = _supplierManager.RetrieveSupplierBySupplierID(supplierID);

            _supplierSchedule.Supplier = supplier;
            GetAvailability(_supplierSchedule.Supplier.SupplierID);

            return View("~/Views/Supplier/ViewSupplierSchedule.cshtml", _supplierSchedule);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// For getting activities by the supplier id passed to it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JsonResult</returns>
        public JsonResult GetActivities(string id)
        {
            int supplierID;
            try
            {
                supplierID = int.Parse(id);
            }
            catch (Exception)
            {

                supplierID = 0;
            }
            List<Activity> activities;
            if (supplierID == 0)
            {
                activities = new List<Activity>();
            }
            else
            {
                activities = _activityManager.RetrieveActivitiesBySupplierID(supplierID);
            }
            return new JsonResult { Data = activities, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// For getting availability by the location id passed to it and sets it to the 
        /// _supplierSchedule
        /// 
        /// Logan Baccam 
        /// </summary>
        /// <param name="id"></param>
        public void GetAvailability(int supplierID = 0)
        {

            List<AvailabilityVM> availability;
            List<Availability> availabilityException;
            if (supplierID == 0)
            {
                availability = new List<AvailabilityVM>();
            }
            else
            {
                availability = _supplierManager.RetrieveSupplierAvailabilityBySupplierID(supplierID);
            }
            if (supplierID == 0)
            {
                availabilityException = new List<Availability>();
            }
            else
            {
                availabilityException = _supplierManager.RetrieveSupplierAvailabilityExceptionBySupplierID(supplierID);
            }

            _supplierSchedule.Availability = availability;
            _supplierSchedule.AvailabilityException = availabilityException;
        }
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/02
        /// 
        /// Description:
        /// For the Supplier details page
        /// 
        /// Christopher Repko
        /// Updated: 2022/04/14
        /// Fixed some issues with wrong comparators, also made tags queried from database
        /// </summary>
        /// <param name="page"></param>
        /// <returns>ActionResult</returns>
        public ActionResult ViewSupplierDetails(int supplierID = 0)
        {
            SupplierDetailsViewModel model = new SupplierDetailsViewModel();
            Supplier supplier = new Supplier();
            List<string> supplierImages = new List<string>();
            List<Reviews> supplierReviews = new List<Reviews>();
            List<string> supplierTags = new List<string>();
            if (supplierID == 0)
            {
                return RedirectToAction("ViewSuppliers", "Supplier");
            }
            try
            {
                supplier = _supplierManager.RetrieveSupplierBySupplierID(supplierID);
                supplierImages = _supplierManager.RetrieveSupplierImagesBySupplierID(supplierID);
                supplierTags = _supplierManager.RetrieveSupplierTagsBySupplierID(supplierID);
                supplierReviews = _supplierManager.RetrieveSupplierReviewsBySupplierID(supplierID);
                if (supplierImages.Count == 0 || supplierImages is null)
                {
                    supplierImages.Add("");
                }
                if (supplierTags.Count == 0)
                {
                    supplierTags.Add("");
                }
                if (supplierReviews.Count == 0)
                {
                    supplierReviews.Add(new Reviews()
                    {
                        Rating = 0,
                        FullName = ""
                    });
                }
                int sum = 0;
                int total = 0;

                foreach (Reviews review in supplierReviews)
                {
                    sum += review.Rating;
                    total++;
                }
                int avg = sum / total;
                supplier.AverageRating = avg;


                model = new SupplierDetailsViewModel();
                model.Supplier = supplier;
                model.SupplierImages = supplierImages;
                model.SupplierReviews = supplierReviews;
                model.SupplierTags = supplierTags;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Supplier not found.");
            }
            return View(model);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/13
        /// 
        /// Description:
        /// For getting the supplier services page
        /// </summary>
        /// <param name="supplier"></param>
        public ActionResult ViewSupplierServices(int supplierID = 0)
        {
            if (supplierID == 0)
            {
                return RedirectToAction("ViewSuppliers", "Supplier");
            }
            SupplierDetailsViewModel _supplier = new SupplierDetailsViewModel();
            List<Service> services = new List<Service>();
            services = _serviceManager.RetrieveServicesBySupplierID(supplierID);
            _supplier.Supplier = _supplierManager.RetrieveSupplierBySupplierID(supplierID);
            List<ServiceVM> serviceVMs = new List<ServiceVM>();
            foreach (Service service in services)
            {
                serviceVMs.Add(new ServiceVM()
                {
                    ServiceID = service.ServiceID,
                    SupplierID = service.SupplierID,
                    ServiceName = service.ServiceName,
                    Price = service.Price,
                    Description = service.Description,
                    ServiceImagePath = service.ServiceImagePath
                });
            }

            _supplier.Services = serviceVMs;

            return View(_supplier);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult ViewSupplierApplications(int page = 1)
        {
            List<Supplier> suppliers = new List<Supplier>();
            SupplierListViewModel model;

            try
            {
                suppliers = _supplierManager.RetrieveUnapprovedSuppliers();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            model = new SupplierListViewModel()
            {
                Suppliers = suppliers.OrderBy(x => x.SupplierID)
                                              .Skip((page - 1) * _pageSize)
                                              .Take(_pageSize),
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = suppliers.Count()
                }
            };
            return View("ViewSupplierApplications", model);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Approve(int supplierID)
        {
            try
            {
                _supplierManager.ApproveSupplier(supplierID);
                Supplier supplier = _supplierManager.RetrieveSupplierBySupplierID(supplierID);

                // This is messy, but there isn't much of a better way.
                User desktopUser = _userManager.RetrieveUserByUserID(supplier.UserID);
                if(desktopUser != null)
                {
                    _userManager.AddUserRole(supplier.UserID, "Supplier");
                    var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    ApplicationUser user = userManager.FindByEmail(desktopUser.EmailAddress);
                    if(user != null)
                    {
                        userManager.AddToRole(user.Id, "Supplier");
                    }
                }
                _emailProvider.SendEmail("Supplier Application", "Your supplier request has been approved and added to the supplier listing.", supplier.Email);
            } catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return ViewSupplierApplications();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Deny(int supplierID)
        {
            try
            {
                _supplierManager.DisapproveSupplier(supplierID);
                Supplier supplier = _supplierManager.RetrieveSupplierBySupplierID(supplierID);
                _emailProvider.SendEmail("Supplier Application", "Your supplier request has been denied. You can find the application in your user profile. Please review the information entered for accuracy and fix any mistakes.", supplier.Email);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return ViewSupplierApplications();
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/28
        /// 
        /// Description
        /// Get handler for ViewUserSuppliers view
        /// </summary>
        /// <param name="userID">ID of user to use for lookup</param>
        /// <param name="page">what page of results to show.</param>
        /// <returns>The ViewSuppliers view with the suppliers related to a specific user loaded.</returns>
        [HttpGet]
        public ActionResult ViewUserSuppliers(int userID, int page=1)
        {
            List<Supplier> _suppliers = new List<Supplier>();
            List<Reviews> _supplierReviews = new List<Reviews>();
            SupplierListViewModel _model = null;

            if (_suppliers is null || _suppliers.Count == 0)
            {
                try
                {
                    _suppliers = _supplierManager.RetrieveSuppliersByUserID(userID);
                    foreach (Supplier supplier in _suppliers)
                    {
                        int avg = 0;
                        int total = 0;
                        _supplierReviews = _supplierManager.RetrieveSupplierReviewsBySupplierID(supplier.SupplierID);
                        if (_supplierReviews.Count != 0)
                        {
                            foreach (Reviews review in _supplierReviews)
                            {
                                avg += review.Rating;
                                total++;
                            }
                            int sum = avg / total;
                            supplier.AverageRating = sum;
                        }
                    }
                    _model = new SupplierListViewModel()
                    {
                        Suppliers = _suppliers.OrderBy(x => x.SupplierID)
                                              .Skip((page - 1) * _pageSize)
                                              .Take(_pageSize),
                        PagingInfo = new PagingInfo()
                        {
                            CurrentPage = page,
                            ItemsPerPage = _pageSize,
                            TotalItems = _suppliers.Count()
                        }
                    };
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return View("ViewSuppliers", _model);
        }
    }
}