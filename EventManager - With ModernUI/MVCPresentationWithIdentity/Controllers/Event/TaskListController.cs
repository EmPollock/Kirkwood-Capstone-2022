using DataObjects;
using LogicLayerInterfaces;
using MVCPresentationWithIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentationWithIdentity.Controllers.Event
{
    /// <summary>
    /// Vinayak Deshpande
    /// Created: 2022/04/26
    /// 
    /// Description:
    /// Controller for looking at list of volunteers assigned to tasks for event.
    /// </summary>
    public class TaskListController : Controller
    {
        // GET: TaskList


        private IEventManager _eventManager;
        private IUserManager _userManager;
        private ITaskManager _taskManager;
        public int _pageSize = 10;
        private List<TaskViewModel> taskViewModels = null;

        public TaskListController(IEventManager eventManager, ITaskManager taskManager, IUserManager userManager)
        {

            _eventManager = eventManager;
            _userManager = userManager;
            _taskManager = taskManager;
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/26
        /// 
        /// Description:
        /// Nav Bar Stuff
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public PartialViewResult EventNavBar(int eventID)
        {
            return PartialView(eventID);
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/26
        /// 
        /// Description:
        /// Generates the EventTaskList
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult EventTaskList(int eventID, int page = 1)
        {
            
            
            List<TasksVM> tasks = new List<TasksVM>();
            TaskListViewModel model = new TaskListViewModel();
            
            if (taskViewModels == null)
            {
                taskViewModels = new List<TaskViewModel>();
                try
                {
                    tasks = _taskManager.RetrieveAllTasksByEventID(eventID);
                    foreach (var task in tasks)
                    {
                        TaskViewModel taskViewModel = new TaskViewModel();
                        taskViewModel.Task = task;
                        taskViewModel.TaskAssignments = _taskManager.RetrieveTaskAssignmentsByTaskID(task.TaskID);
                        taskViewModels.Add(taskViewModel);
                    }
                    
                    model = new TaskListViewModel
                    {
                        Tasks = taskViewModels
                                            .Skip((page - 1) * _pageSize)
                                            .Take(_pageSize),
                        PagingInfo = new PagingInfo
                        {
                            CurrentPage = page,
                            ItemsPerPage = _pageSize,
                            TotalItems = taskViewModels.Count()
                        },
                        EventName = _eventManager.RetrieveEventByEventID(eventID).EventName
                    };
                }
                catch (Exception ex)
                {

                    TempData["errorMessage"] = ex.Message;
                }

                

            }
            return View(model);
        }
    }
}