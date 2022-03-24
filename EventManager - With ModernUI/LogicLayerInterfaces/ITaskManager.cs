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
        bool AddTask(Tasks newTask, int numTotalVolunteers);
        bool EditTask(Tasks oldTask, Tasks newTasks);
        List<Priority> RetrieveAllPriorities();
        List<TasksVM> RetrieveAllActiveTasksByEventID(int eventID = 100000);

        List<TaskAssignmentVM> RetrieveTaskAssignmentsByTaskID(int taskID);
    }
}
