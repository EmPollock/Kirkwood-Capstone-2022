using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayer;
using MVCPresentationWithIdentity.Models;

namespace MVCPresentationWithIdentity.Controllers
{
    public class LocationController : Controller
    {
        private LocationManager _locationManager = new LocationManager();
        private EntranceManager _entranceManager = new EntranceManager();

        // GET: Location
        public ActionResult Index()
        {
            return View(_locationManager.RetrieveActiveLocations());
        }

        // GET: Location/Details/5
        public ActionResult Details(int id)
        {
            Location @location = _locationManager.RetrieveActiveLocations().FirstOrDefault(x => x.LocationID == id);
            if (@location == null)
            {
                return HttpNotFound();
            }
            return View(@location);
        }


        /// <summary>
        /// Alaina Gilson
        /// Created: 2022-04-11
        /// 
        /// Description:
        /// Displays all entrances for a specific location
        /// </summary>
        /// <param name="locationID"></param>
        // GET: Location/EntranceIndex
        public ActionResult EntranceIndex(int locationID)
        {
            List<EntranceModel> model = new List<EntranceModel>();
            List<Entrance> entrances = _entranceManager.RetrieveEntranceByLocationID(locationID);
            if(entrances.Count() == 0)
            {
                entrances.Add(new Entrance()
                {
                    LocationID = locationID,
                    EntranceID = -1,
                    EntranceName = "",
                    Description = ""
                });
            }
            else
            {
                foreach(var entrance in entrances)
                {
                    model.Add(new EntranceModel()
                    {
                        LocationID = locationID,
                        EntranceID = entrance.EntranceID,
                        EntranceName = entrance.EntranceName,
                        Description = entrance.Description
                    });
                }
            }
            return View(model);
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022-04-11
        /// 
        /// Description:
        /// Redirects to EntranceEdit if a user clicks on the "create" buttons
        /// </summary>
        /// <param name="locationID"></param>
        // GET: Location/EntranceCreate
        public ActionResult EntranceCreate(int locationID)
        {
            ViewBag.Name = "Create Entrance";
            EditEntranceModel model = new EditEntranceModel()
            {
                LocationID = locationID,
                EntranceID = -1,
                OldEntranceName = "",
                OldDescription = "",
                EntranceName = "",
                Description = ""
            };
            return View("EntranceEdit", model);
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022-04-11
        /// 
        /// Description:
        /// Displays proper labels depending on create or edit mode
        /// </summary>
        /// <param name="locationID"></param>
        /// <param name="entranceID"></param>
        // GET: Location/EntranceEdit
        public ActionResult EntranceEdit(int locationID, int entranceID)
        {
            ViewBag.Name = "Edit Entrance";
            Entrance entrance = _entranceManager.RetrieveEntranceByLocationID(locationID).FirstOrDefault(x => x.EntranceID == entranceID);
            EditEntranceModel model = new EditEntranceModel()
            {
                LocationID = locationID,
                EntranceID = entranceID,
                OldEntranceName = entrance.EntranceName,
                OldDescription = entrance.Description,
                EntranceName = entrance.EntranceName,
                Description = entrance.Description
            };
            return View(model);
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022-04-11
        /// 
        /// Description:
        /// POST method to allow user to edit a current entrance or create a new one
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EntranceEdit(EditEntranceModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    if (_entranceManager.RetrieveEntranceByLocationID(model.LocationID).Where(e => e.EntranceID == model.EntranceID).Count() != 0)
                    {
                        Entrance oldEntrance = new Entrance()
                        {
                            LocationID = model.LocationID,
                            EntranceID = model.EntranceID,
                            EntranceName = model.OldEntranceName,
                            Description = model.OldDescription
                        };
                        Entrance newEntrance = new Entrance()
                        {
                            LocationID = model.LocationID,
                            EntranceID = model.EntranceID,
                            EntranceName = model.EntranceName,
                            Description = model.Description
                        };

                        _entranceManager.UpdateEntrance(oldEntrance, newEntrance);
                    }
                    else
                    {
                        _entranceManager.CreateEntrance(model.LocationID, model.EntranceName, model.Description);
                    }

                    return RedirectToAction("EntranceIndex", new { locationID = model.LocationID });
                }
                catch(Exception ex)
                {
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        /// <summary>
        /// Alaina Gilson 
        /// Created: 2022-04-11
        /// 
        /// Description: 
        /// Allows "deleting" of an entrance
        /// </summary>
        /// <param name="locationID"></param>
        /// <param name="entranceID"></param>
        [HttpPost]
        public ActionResult EntranceDelete(int locationID, int entranceID)
        {
            Entrance entrance = _entranceManager.RetrieveEntranceByLocationID(locationID).FirstOrDefault(x => x.EntranceID == entranceID);

            if(entrance != null)
            {
                _entranceManager.RemoveEntranceByEntranceID(entranceID);
            }

            return RedirectToAction("EntranceIndex", new { locationID = locationID });
        }
    }
}
