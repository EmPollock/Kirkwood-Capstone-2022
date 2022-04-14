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
    public class SupplierController : Controller
    {
        ISupplierManager _supplierManager;
        IActivityManager _activityManager = null;
        SupplierScheduleViewModel _supplierSchedule = new SupplierScheduleViewModel();
        public int _pageSize = 10;


        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/02
        /// 
        /// Description:
        /// Default constructor for the Supplier controller
        /// </summary>
        public SupplierController(ISupplierManager supplierManager, IActivityManager activityManager)
        {
            _supplierManager = supplierManager;
            _activityManager = activityManager;
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
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult ViewSupplierSchedule(Supplier supplier)
        {
            //Request.Params["supplier"];
            
            if(supplier.SupplierID == 0)
            {
                
                return RedirectToAction("ViewSuppliers", "Supplier");
            }

            _supplierSchedule.Supplier = supplier;
            GetAvailability(supplier.SupplierID);

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
            int supplierID ;
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
                availability = _supplierManager.RetrieveSupplierAvailabilityBySupplierID(id);
            }
            if(id == 0)
            {
                availabilityException = new List<Availability>();
            }
            else
            {
                availabilityException = _supplierManager.RetrieveSupplierAvailabilityExceptionBySupplierID(id);
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
        /// </summary>
        /// <param name="page"></param>
        /// <returns>ActionResult</returns>
        public ActionResult ViewSupplierDetails(int supplierID)
        {
            SupplierDetailsViewModel model = null;
            Supplier supplier = new Supplier();
            List<string> supplierImages = new List<string>();
            List<Reviews> supplierReviews = new List<Reviews>();
            List<string> supplierTags = new List<string>();

            try
            {
                if (model is null)
                {
                    supplier = _supplierManager.RetrieveSupplierBySupplierID(supplierID);
                    supplierImages = _supplierManager.RetrieveSupplierImagesBySupplierID(supplierID);
                    if (supplierImages.Count > 0 || supplierImages is null)
                    {
                        supplierImages.Add("");
                    }
                    supplierReviews = _supplierManager.RetrieveSupplierReviewsBySupplierID(supplierID);
                    if (supplierReviews.Count > 0)
                    {
                        Reviews noReview = new Reviews()
                        {
                            Rating = 0,
                            FullName = ""
                        };
                    }
                    if (supplierTags.Count > 0)
                    {
                        supplierTags.Add("");
                    }
                    if (supplierReviews.Count != 0)
                    {
                        int avg = 0;
                        int total = 0;

                        foreach (Reviews review in supplierReviews)
                        {
                            avg += review.Rating;
                            total++;
                        }
                        int sum = avg / total;
                        supplier.AverageRating = sum;
                    }
                }
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
    }
}