﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;

namespace LogicLayerInterfaces
{
    public interface ITaskManager
    {
        bool AddTask(Tasks newTask, int numTotalVolunteers);
        bool EditTask(Tasks oldTask, Tasks newTasks);
        List<Priority> RetrieveAllPriorities();
        List<TasksVM> RetrieveAllActiveTasksByEventID(int eventID);
        bool UserCanEditDeleteTask(int userID);
        bool RemoveTaskByTaskID(int taskID);
        List<TaskAssignmentVM> RetrieveTaskAssignmentsByTaskID(int taskID);
        List<TasksVM> RetrieveAllTasksByEventID(int eventID);
    }
}
