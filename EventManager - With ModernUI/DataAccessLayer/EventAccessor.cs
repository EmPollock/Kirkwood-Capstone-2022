using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class EventAccessor : IEventAccessor
    {
        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Insert event into tadpole_db
        /// 
        /// </summary>
        /// <param name="eventName">Name of the event</param>
        /// <param name="eventDescription">Description fo the event</param>
        /// <param name="locationID">Description fo the event</param>
        /// <returns>Number of rows inserted</returns>
        public int InsertEvent(string eventName, string eventDescription, int locationID)
        {
            int rowsAffected = 0;

            // connection
            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_insert_event";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EventName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@EventDescription", SqlDbType.NVarChar, 1000);
            cmd.Parameters.Add("@LocationID", SqlDbType.Int);

            cmd.Parameters["@EventName"].Value = eventName;
            cmd.Parameters["@EventDescription"].Value = eventDescription;
            cmd.Parameters["@LocationID"].Value = locationID;


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
        /// Jace Pettinger
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Select active events from tadpole_db
        /// 
        /// </summary>
        /// <returns>List of active events</returns>
        public List<Event> SelectActiveEvents()
        {
            List<Event> events = new List<Event>();
           
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_active_events";
            
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
                        events.Add(new Event()
                        {
                            EventID = reader.GetInt32(0),
                            EventName = reader.GetString(1),
                            EventDescription = reader.GetString(2),
                            EventCreatedDate = reader.GetDateTime(3),
                            Active = true
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return events;
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/02
        /// 
        /// Description:
        /// Updates a record in the Event table
        /// 
        /// </summary>
        /// <returns>int rows affected</returns>
        public int UpdateEvent(Event oldEvent, Event newEvent)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_update_event_by_eventID";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EventID", oldEvent.EventID);
            cmd.Parameters.Add("@OldEventName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@OldEventName"].Value = oldEvent.EventName;
            cmd.Parameters.Add("@OldEventDescription", SqlDbType.NVarChar, 1000);
            cmd.Parameters["@OldEventDescription"].Value = oldEvent.EventDescription;
            cmd.Parameters.Add("@OldActive", SqlDbType.Bit);
            cmd.Parameters["@OldActive"].Value = oldEvent.Active;
            cmd.Parameters.Add("@NewEventName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@NewEventName"].Value = newEvent.EventName;
            cmd.Parameters.Add("@NewEventDescription", SqlDbType.NVarChar, 1000);
            cmd.Parameters["@NewEventDescription"].Value = newEvent.EventDescription;
            cmd.Parameters.Add("@NewActive", SqlDbType.Bit);
            cmd.Parameters["@NewActive"].Value = newEvent.Active;

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
        public Event SelectEventByEventNameAndDescription(string eventName, string eventDescription)
        {
            Event eventToGet = null;

            // connection
            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_select_event_by_event_name_and_description";
        var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EventName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@EventDescription", SqlDbType.NVarChar, 1000);

            cmd.Parameters["@EventName"].Value = eventName;
            cmd.Parameters["@EventDescription"].Value = eventDescription;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                         eventToGet = new Event()
                        {
                            EventID = reader.GetInt32(0),
                            EventName = reader.GetString(1),
                            EventDescription = reader.GetString(2),
                            EventCreatedDate = reader.GetDateTime(3),
                            Active = true
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return eventToGet;
        }
    }
}
