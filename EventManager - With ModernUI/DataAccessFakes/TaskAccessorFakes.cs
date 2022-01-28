using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    /// <summary>
    /// Mike Cahow
    /// Created: 2022/01/24
    /// 
    /// Description:
    /// Class of fakes used to test the methods related to Tasks
    /// </summary>
    public class TaskAccessorFakes : ITaskAccessor
    {
        private List<Tasks> FakeTasks = new List<Tasks>();

        public TaskAccessorFakes()
        {
            FakeTasks.Add(new Tasks()
            {
                EventID = 999999,
                Name = "Sweep up",
                Description = "Sweep up a mess pls",
                DueDate = DateTime.Now,
                Priority = 1
            });

            FakeTasks.Add(new Tasks()
            {
                EventID = 999999,
                Name = "Mop",
                Description = "Mop up spilled drink",
                DueDate = DateTime.Now,
                Priority = 3
            });

            FakeTasks.Add(new Tasks()
            {
                EventID = 999999,
                Name = "Vaccum",
                Description = "Someone dropped food on the carpet again. Vaccum please.",
                DueDate = DateTime.Now,
                Priority = 1
            });
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// Fake insert tasks method used for testing
        /// </summary>
        /// <param name="newTask"></param>
        /// <returns>int rowsAffected</returns>
        public int InsertTasks(Tasks newTask)
        {
            int rowsAffected = 0;

            try
            {
                FakeTasks.Add(new Tasks()
                {
                    EventID = 999999,
                    Name = "Clean",
                    Description = "Clean this room.",
                    DueDate = DateTime.Now,
                    Priority = 3
                });
                rowsAffected = 1;
            }
            catch (Exception)
            {

                throw;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// A method that returns a fake list of priorites
        /// </summary>
        /// <returns>List Priority</returns>
        public List<Priority> SelectAllPriorities()
        {
            throw new NotImplementedException();
        }
    }
}
