using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MVCPresentationWithIdentity.Models;
using LogicLayer;
using LogicLayerInterfaces;
using DataObjects;


namespace MVCPresentationWithIdentity.Controllers.Locations
{
    public class ParkingLotController : Controller
    {
        IParkingLotManager _parkingLotManager;
        ILocationManager _locationManager;

        public ParkingLotController(IParkingLotManager parking, ILocationManager location)
        {
            _parkingLotManager = parking;
            _locationManager = location;
        }
        // GET: PartkingLot
        public ActionResult Index(int locationID)
        {
            ParkingLotModel model = new ParkingLotModel();
            model.ParkingLots = new List<ParkingLot>();

            
            bool canEdit = false;
            try
            {
                List<ParkingLotVM> lots = _parkingLotManager.RetrieveParkingLotByLocationID(locationID);

                Location location = _locationManager.RetrieveLocationByLocationID(locationID);
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser applicationUser = userManager.FindById(User.Identity.GetUserId());
                if (applicationUser != null && applicationUser.UserID == location.UserID)
                {
                    canEdit = true;
                }

                foreach (ParkingLot lot in lots)
                {
                    model.ParkingLots.Add(new ParkingLot()
                    {
                        LotID = lot.LotID,
                        LocationID = lot.LocationID,
                        Name = lot.Name,
                        Description = lot.Description,
                        ImageName = lot.ImageName
                    });
                }

                model.LocationID = location.LocationID;
                model.LocationName = location.Name;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Failed to retrieve areas: " + ex.Message);
            }
            model.CanEdit = canEdit;
            return View(model);
        }

        // GET: ParkingLot/Create
        public ActionResult Create(int locationId)
        {
            return View("Edit", new EditParkingLotModel()
            {
                LocationID = locationId,
                LotID = -1,
                Description = "",
                ImageName = "",
                NewDescription = "",
                Name = "",
                NewName = ""
            });
        }

        // GET: ParkingLot/Edit/5
        public ActionResult Edit(int lotId)
        {
            try
            {
                var lotManager = new ParkingLotManager();
                ParkingLot parkingLot = lotManager.RetrieveParkingLotByLotID(lotId);
                EditParkingLotModel model = new EditParkingLotModel()
                {
                    LocationID = parkingLot.LocationID,
                    LotID = parkingLot.LotID,
                    Name = parkingLot.Name,
                    Description = parkingLot.Description,
                    ImageName = parkingLot.ImageName,
                    NewName = parkingLot.Name,
                    NewDescription = parkingLot.Description
                };
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        // POST: ParkingLot/Edit/5
        [HttpPost]
        public ActionResult Edit(EditParkingLotModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.NewImage != null)
                {
                    string uuid = Guid.NewGuid().ToString();
                    model.NewImage.SaveAs(Server.MapPath("~/Content/Images/LocationImages/") + uuid + Path.GetExtension(model.NewImage.FileName));
                    model.ImageName = uuid + Path.GetExtension(model.NewImage.FileName);
                }
                try
                {
                    var lotManager = new ParkingLotManager();
                    if (lotManager.RetrieveParkingLotByLotID(model.LotID) != null)
                    {
                        var oldParkingLot = new ParkingLot()
                        {
                            LocationID = model.LocationID,
                            LotID = model.LotID,
                            Name = model.Name,
                            Description = model.Description,
                        };
                        var newParkingLot = new ParkingLot()
                        {
                            LocationID = model.LocationID,
                            LotID = model.LotID,
                            Name = model.NewName,
                            Description = model.NewDescription,
                            ImageName = model.ImageName
                        };
                        lotManager.EditParkingLotByLotID(model.LotID, oldParkingLot, newParkingLot);
                    }
                    else
                    {
                        var lot = new ParkingLot()
                        {
                            LocationID = model.LocationID,
                            LotID = model.LotID,
                            Name = model.NewName,
                            Description = model.NewDescription,
                            ImageName = model.ImageName
                        };
                        lotManager.CreateParkingLot(lot);
                    }
                    return RedirectToAction("Index", new { locationId = model.LocationID });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }

        }

        // POST: ParkingLot/Delete/5
        [HttpPost]
        public ActionResult Delete(int lotId, int locationId)
        {
            try
            {
                var sublocationManager = new ParkingLotManager();
                sublocationManager.RemoveParkingLotByLotID(lotId);

                return RedirectToAction("Index", new { locationId = locationId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Failed to delete area: " + ex.Message);
                return RedirectToAction("Index", new { locationId = locationId });
            }
        }
    }
}