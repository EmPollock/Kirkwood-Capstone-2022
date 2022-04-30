﻿using System;
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
    public class SupplierController : Controller
    {
        ISupplierManager _supplierManager;
        IActivityManager _activityManager = null;
        IServiceManager _serviceManager = null;
        IUserManager _userManger;
        SupplierScheduleViewModel _supplierDetails = new SupplierScheduleViewModel();
        public int _pageSize = 10;


        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/02
        /// 
        /// Description:
        /// Default constructor for the Supplier controller
        /// 
        /// <update>
        /// Emma Pollock
        /// Updated: 2022/04/27
        /// 
        /// Added IUserManager
        /// </update>
        /// </summary>
        public SupplierController(ISupplierManager supplierManager, IActivityManager activityManager, IServiceManager serviceManager, IUserManager userManager)
        {
            _supplierManager = supplierManager;
            _activityManager = activityManager;
            _serviceManager = serviceManager;
            _userManger = userManager;
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
                    TempData["errorMessage"] = ex.Message;
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

            _supplierDetails.Supplier = supplier;
            GetAvailability(_supplierDetails.Supplier.SupplierID);

            return View("~/Views/Supplier/ViewSupplierSchedule.cshtml", _supplierDetails);
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

            _supplierDetails.Availability = availability;
            _supplierDetails.AvailabilityException = availabilityException;
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
                ModelState.AddModelError("", "Location not found.");
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

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/04/27
        /// 
        /// Description:
        /// Moves user to the Create Review page
        /// </summary>
        /// <param name="supplierID"></param>
        [Authorize(Roles = "Event Planner")]
        [HttpGet]
        public ViewResult CreateReview(int supplierID = 0)
        {
            if(supplierID == 0)
            {
                return View("ViewSuppliers", supplierID);
            }
            return View(new Reviews() { ForeignID = supplierID });
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/04/27
        /// 
        /// Description:
        /// Processes the review to be created and either returns a validation error or creates the review
        /// </summary>
        ///
        /// <param name="ForeignID"></param>
        /// <param name="Rating"></param>
        /// <param name="Review"></param>
        [HttpPost]
        public ActionResult CreateReview(int ForeignID, int Rating, String Review)
        {
            User user = _userManger.RetrieveUserByEmail(User.Identity.Name);
            Reviews review = new Reviews() {ForeignID = ForeignID, UserID = user.UserID, Rating = Rating, Review = Review, ReviewType = "Supplier", DateCreated = DateTime.Now}; 
            if (ModelState.IsValid)
            {
                try
                {
                    _supplierManager.CreateSupplierReview(review);
                } catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(review);
                }
                return RedirectToAction("ViewSuppliers", "Supplier", review.ForeignID);
            }
            return View(review);            
        }
    }
}