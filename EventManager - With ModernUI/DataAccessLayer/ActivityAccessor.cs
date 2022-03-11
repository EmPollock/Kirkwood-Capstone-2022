using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace DataAccessLayer
{
    public class ActivityAccessor : IActivityAccessor
    {
        public int InsertActivity(Activity activity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Returns the list of Activities for an event
        /// 
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns>A list of Activity objects</returns>
        /// /// <summary>
        /// Logan Baccam
        /// Updated: 2022/02/25
        /// Description:
        /// Reverted changes
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns>A list of Activity objects</returns>
        public List<Activity> SelectActivitiesByEventID(int eventID)
        {
            List<Activity> result = new List<Activity>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_activities_by_eventID";

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
                        /*
                                [ActivityID]			
                                ,[ActivityName]			
                                ,[ActivityDescription]	
                                ,[PublicActivity]		
                                ,[StartTime]			
                                ,[EndTime]				
                                ,[ActivityImageName]	
                                ,[SublocationID]		
                                ,[EventDateID]		
                         */
                        result.Add(new ActivityVM()
                        {
                            ActivityID = reader.GetInt32(0),
                            ActivityName = reader.GetString(1),
                            ActivityDescription = reader.GetString(2),
                            PublicActivity = reader.GetBoolean(3),
                            StartTime = DateTime.ParseExact(reader["StartTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            EndTime = DateTime.ParseExact(reader["EndTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            ActivityImageName = reader.IsDBNull(6) ? null : reader.GetString(6),
                            SublocationID = reader.GetInt32(7),
                            EventDateID = DateTime.Parse(reader[8].ToString())
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// Returns the list of Activities for a date of an event
        /// 
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="eventDateID"></param>
        /// <returns>A list of Activity objects</returns>
        public List<Activity> SelectActivitiesByEventIDAndEventDateID(int eventID, DateTime? eventDateID)
        {
            List<Activity> result = new List<Activity>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_activities_by_eventID_and_event_dateID";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EventID", SqlDbType.Int);
            cmd.Parameters.Add("@EventDateID", SqlDbType.Date);

            cmd.Parameters["@EventID"].Value = eventID;
            cmd.Parameters["@EventDateID"].Value = eventDateID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        /*
                                [ActivityID]			
                                ,[ActivityName]			
                                ,[ActivityDescription]	
                                ,[PublicActivity]		
                                ,[StartTime]			
                                ,[EndTime]				
                                ,[ActivityImageName]	
                                ,[SublocationID]			
                         */
                        result.Add(new Activity()
                        {
                            ActivityID = reader.GetInt32(0),
                            ActivityName = reader.GetString(1),
                            ActivityDescription = reader.GetString(2),
                            PublicActivity = reader.GetBoolean(3),
                            StartTime = DateTime.ParseExact(reader["StartTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            EndTime = DateTime.ParseExact(reader["EndTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            ActivityImageName = reader.IsDBNull(6) ? null : reader.GetString(6),
                            SublocationID = reader.GetInt32(7),
                            EventDateID = (DateTime)eventDateID
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }


        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/23
        /// 
        /// Description:
        /// Select Activities for a specific sublocationID passed to it
        /// 
        /// </summary>
        /// <param name="sublocationID"></param>
        /// <returns>A List of Activities</returns>
        public List<Activity> SelectActivitiesBySublocationID(int sublocationID)
        {
            List<Activity> result = new List<Activity>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_activities_by_sublocationID";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SublocationID", SqlDbType.Int);

            cmd.Parameters["@SublocationID"].Value = sublocationID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        /*
                                [ActivityID]			
                                ,[ActivityName]			
                                ,[ActivityDescription]	
                                ,[PublicActivity]		
                                ,[StartTime]			
                                ,[EndTime]				
                                ,[ActivityImageName]	
                                ,[SublocationID]			
                         */
                        result.Add(new Activity()
                        {
                            ActivityID = reader.GetInt32(0),
                            ActivityName = reader.GetString(1),
                            PublicActivity = reader.GetBoolean(2),
                            StartTime = DateTime.ParseExact(reader["StartTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            EndTime = DateTime.ParseExact(reader["EndTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            EventDateID = DateTime.Parse(reader["EventDateID"].ToString())
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }


        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/24
        /// 
        /// Description:
        /// Returns the list of Activities for an event in a view model
        /// 
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns>A list of ActivityVM objects</returns>
        public List<ActivityVM> SelectActivitiesByEventIDForVM(int eventID)
        {
            List<ActivityVM> result = new List<ActivityVM>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_activities_by_eventID_for_activityvm";

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
                       
                        result.Add(new ActivityVM()
                        {
                            ActivityID = reader.GetInt32(0),
                            ActivityName = reader.GetString(1),
                            ActivityDescription = reader.GetString(2),
                            PublicActivity = reader.GetBoolean(3),
                            StartTime = DateTime.ParseExact(reader["StartTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            EndTime = DateTime.ParseExact(reader["EndTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            ActivityImageName = reader.IsDBNull(6) ? null : reader.GetString(6),
                            SublocationID = reader.GetInt32(7),
                            EventDateID = DateTime.Parse(reader[8].ToString()),
                            SublocationName = reader.GetString(9)
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }


        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/14
        /// 
        /// Description:
        /// Returns the list of all public activities from the activity table in a view model
        /// 
        /// </summary>
        /// 
        /// <returns>A list of Activity objects</returns>
        public List<ActivityVM> SelectActivitiesPastAndUpcomingDates()
        {
            List<ActivityVM> activeActivities = new List<ActivityVM>();

            var cmdText = "sp_select_activities_for_past_and_upcoming_dates";
            var conn = DBConnection.GetConnection();
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
                        activeActivities.Add(new ActivityVM()
                        {

                            ActivityID = reader.GetInt32(0),
                            ActivityName = reader.GetString(1),
                            ActivityDescription = reader.GetString(2),
                            StartTime = DateTime.ParseExact(reader["StartTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            EndTime = DateTime.ParseExact(reader["EndTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            ActivityImageName = reader.IsDBNull(5) ? null : reader.GetString(5),
                            SublocationID = reader.GetInt32(6),
                            SublocationName = reader.GetString(7),
                            EventDateID = reader.GetDateTime(9),
                            PublicActivity = true

                        });
                    }
                }
            }
            catch (Exception ex) { throw ex; }

            return activeActivities;
        }
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/12
        /// 
        /// Description:
        /// Returns the list of all upcoming and past activities
        /// 
        /// </summary>
        /// 
        /// <returns>A list of Activity objects</returns>
        public List<ActivityVM> SelectUserActivitiesPastAndUpcomingDates(int userID)
        {
            List<ActivityVM> activeActivities = new List<ActivityVM>();

            var cmdText = "sp_select_all_activities_for_user";
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        activeActivities.Add(new ActivityVM()
                        {
                            ActivityID = reader.GetInt32(0),
                            ActivityName = reader.GetString(1),
                            ActivityDescription = reader.GetString(2),
                            StartTime = DateTime.ParseExact(reader["StartTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            EndTime = DateTime.ParseExact(reader["EndTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            ActivityImageName = reader.IsDBNull(5) ? null : reader.GetString(5),
                            SublocationID = reader.GetInt32(6),
                            SublocationName = reader.GetString(7),
                            EventID = reader.GetInt32(8),
                            EventDateID = reader.GetDateTime(9),
                            PublicActivity = reader.GetBoolean(11)


                        });

                    }
                }
            }
            catch (Exception ex) { throw ex; }
            return activeActivities;
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/02/24
        /// 
        /// Description:
        /// Returns a list of activities which are associated with a specific supplier
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <param name="date"></param>
        /// <returns>A list of activity objects</returns>
        public List<Activity> SelectActivitiesBySupplierIDAndDate(int supplierID, DateTime date)
        {
            List<Activity> result = new List<Activity>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_activities_by_supplierID_and_date";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
            cmd.Parameters["@SupplierID"].Value = supplierID;
            cmd.Parameters.Add("@ActivityDate", SqlDbType.Date);
            cmd.Parameters["@ActivityDate"].Value = date;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new Activity()
                        {
                            ActivityID = reader.GetInt32(0),
                            ActivityName = reader.GetString(1),
                            ActivityDescription = reader.GetString(2),
                            PublicActivity = reader.GetBoolean(3),
                            StartTime = DateTime.ParseExact(reader["StartTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            EndTime = DateTime.ParseExact(reader["EndTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            ActivityImageName = reader.IsDBNull(6) ? null : reader.GetString(6),
                            SublocationID = reader.GetInt32(7),
                            EventDateID = DateTime.Parse(reader[8].ToString()),
                            EventID = reader.GetInt32(9)
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
    }
}
