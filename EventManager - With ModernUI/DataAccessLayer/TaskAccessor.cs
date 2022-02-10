using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using DataAccessInterfaces;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Mike Cahow
    /// Created: 2022/01/23
    /// 
    /// Description:
    /// Accessor class that reads in the data from the stored procedures created for Tasks
    /// </summary>
    public class TaskAccessor : ITaskAccessor
    {
        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// A method for inserting a new task into the database
        /// </summary>
        /// <param name="newTask"></param>
        /// <returns>int rowsAffected = 1</returns>
        public int InsertTasks(Tasks newTask)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_insert_new_task_by_eventID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EventID", SqlDbType.Int);
            cmd.Parameters["@EventID"].Value = newTask.EventID;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50);
            cmd.Parameters["@Name"].Value = newTask.Name;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 255);
            cmd.Parameters["@Description"].Value = newTask.Description;
            cmd.Parameters.Add("@DueDate", SqlDbType.DateTime);
            cmd.Parameters["@DueDate"].Value = newTask.DueDate;
            cmd.Parameters.Add("@Priority", SqlDbType.Int);
            cmd.Parameters["@Priority"].Value = newTask.Priority;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// A method that returns a list of Priorities
        /// </summary>
        /// <returns>A list of Priorities (Only 3)</returns>
        public List<Priority> SelectAllPriorities()
        {
            List<Priority> priorities = new List<Priority>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_all_priorities";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        priorities.Add(new Priority()
                        {
                            PriorityID = reader.GetInt32(0),
                            Description = reader.GetString(1)
                        });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return priorities;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/31
        /// 
        /// Description:
        /// Select method that grabs a list of all tasks for an event
        /// </summary>
        /// <returns>List Tasks</returns>
        public List<TasksVM> SelectAllActiveTasksByEventID(int eventID)
        {
            List<TasksVM> tasks = new List<TasksVM>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_active_tasks_by_eventID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EventID", SqlDbType.Int);
            cmd.Parameters["@EventID"].Value = eventID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tasks.Add(new TasksVM()
                        {
                            TaskID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            DueDate = reader.GetDateTime(3),
                            TaskPriority = reader.GetString(4),
                            TaskEventName = reader.GetString(5),
                            Active = true
                        });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return tasks;
        }
    }
}
