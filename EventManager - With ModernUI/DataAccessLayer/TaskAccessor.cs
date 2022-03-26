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
        /// 
        /// Vinayak Deshpande
        /// Update: 2022/02/28
        /// Description: Adding functionality to return taskID instead of rows affected.
        /// and then create volunteer need


        public int InsertTasks(Tasks newTask, int numTotalVolunteers)
        {
            int rowsAffected = 0;
            int taskID;

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
                taskID = Convert.ToInt32(cmd.ExecuteScalar());
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            cmdText = "sp_insert_new_volunteer_need";
            var cmd2 = new SqlCommand(cmdText, conn);
            cmd2.CommandType = CommandType.StoredProcedure;

            cmd2.Parameters.Add("@TaskID", SqlDbType.Int);
            cmd2.Parameters["@TaskID"].Value = taskID;
            cmd2.Parameters.Add("@NumTotalVolunteers", SqlDbType.Int);
            cmd2.Parameters["@NumTotalVolunteers"].Value = numTotalVolunteers;

            try
            {
                conn.Open();
                rowsAffected = cmd2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// A method for updating a task object in the database
        /// </summary>
        /// <param name="oldTask"></param>
        /// <param name="newTask"></param>
        /// <returns></returns>
        public int UpdateTasks(Tasks oldTask, Tasks newTask)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_update_task";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EventID", SqlDbType.Int);
            cmd.Parameters["@EventID"].Value = newTask.EventID;
            cmd.Parameters.Add("@TaskID", SqlDbType.Int);
            cmd.Parameters["@TaskID"].Value = oldTask.TaskID;
            cmd.Parameters.AddWithValue("@OldName", oldTask.Name);
            cmd.Parameters.AddWithValue("OldDescription", oldTask.Description);
            cmd.Parameters.AddWithValue("OldDueDate", oldTask.DueDate);
            cmd.Parameters.AddWithValue("@OldPriority", oldTask.Priority);
            cmd.Parameters.AddWithValue("OldActive", oldTask.Active);
            cmd.Parameters.Add("@NewName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@NewName"].Value = newTask.Name;
            cmd.Parameters.Add("@NewDescription", SqlDbType.NVarChar, 255);
            cmd.Parameters["@NewDescription"].Value = newTask.Description;
            cmd.Parameters.Add("@NewDueDate", SqlDbType.DateTime);
            cmd.Parameters["@NewDueDate"].Value = newTask.DueDate;
            cmd.Parameters.Add("@NewPriority", SqlDbType.Int);
            cmd.Parameters["@NewPriority"].Value = newTask.Priority;
            cmd.Parameters.Add("@NewActive", SqlDbType.Bit);
            cmd.Parameters["@NewActive"].Value = newTask.Active;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
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
                            Priority = reader.GetInt32(4),
                            TaskPriority = reader.GetString(5),
                            TaskEventName = reader.GetString(6),
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

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Select method that gets a list of taskAssginments for a task
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns>List Tasks</returns>
        public List<TaskAssignmentVM> SelectTaskAssignmentsByTaskID(int taskID)
        {
            List<TaskAssignmentVM> taskAssignments = new List<TaskAssignmentVM>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_task_assignments_by_task_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@TaskID", SqlDbType.Int);
            cmd.Parameters["@TaskID"].Value = taskID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        taskAssignments.Add(new TaskAssignmentVM()
                        {
                            /*
                              [TaskAssignmentID]	
                              [DateAssigned]		 				 		 
                              [UserID]			 
                              [RoleID]	
                              [GivenName]
                              [FamilyName]
                            */
                            TaskAssignmentID = reader.GetInt32(0),
                            DateAssigned = DateTime.Parse(reader[1].ToString()),
                            TaskID = taskID,
                            UserID = reader.GetInt32(2),
                            RoleID = reader.GetString(3),
                            Name = reader.GetString(4) + " " + reader.GetString(5)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return taskAssignments;
        }
    }
}
