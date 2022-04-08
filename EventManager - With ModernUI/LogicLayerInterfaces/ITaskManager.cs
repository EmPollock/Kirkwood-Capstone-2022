using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;

namespace LogicLayerInterfaces
{
    public interface ITaskManager
    {
        int AddTask(Tasks newTask, int numTotalVolunteers);
        bool EditTask(Tasks oldTask, Tasks newTasks);
        List<Priority> RetrieveAllPriorities();
        List<TasksVM> RetrieveAllActiveTasksByEventID(int eventID = 100000);
        int AddTaskAssignment(int taskID);
        bool AddVolunteerToTaskAssignment(int taskAssignmentID, int userID);
    }
}
