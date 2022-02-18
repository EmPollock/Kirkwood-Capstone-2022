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
        /// <returns>Number of rows inserted</returns>
        public int InsertEvent(string eventName, string eventDescription)
        {
            int rowsAffected = 0;

            // connection
            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_insert_event";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EventName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@EventDescription", SqlDbType.NVarChar, 1000);
            
            cmd.Parameters["@EventName"].Value = eventName;
            cmd.Parameters["@EventDescription"].Value = eventDescription;
            

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
        /// Jace Pettinger
        /// Updated: 2022/02/15
        /// 
        /// Description:
        /// Adding locationID to the returned event object
        /// 
        /// </summary>
        /// <returns>List of active events</returns>
        public List<EventVM> SelectActiveEvents()
        {
            List<EventVM> events = new List<EventVM>();

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
                        events.Add(new EventVM()
                        {
                            EventID = reader.GetInt32(0),
                            EventName = reader.GetString(1),
                            EventDescription = reader.GetString(2),
                            EventCreatedDate = reader.GetDateTime(3),
                            LocationID = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4),
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
        public int UpdateEvent(EventVM oldEvent, EventVM newEvent)
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

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Retrieve an event object by its nabe and Description
        /// 
        /// Jace Pettinger
        /// Updated: 2022/02/15
        /// 
        /// Description:
        /// Adding locationID to the returned event object
        /// 
        /// </summary>
        /// <param name="eventName">The name of the event</param>
        /// <param name="eventDescription">The description of the event</param>
        /// <returns></returns>
        public EventVM SelectEventByEventNameAndDescription(string eventName, string eventDescription)
        {
            EventVM eventToGet = null;

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
                        eventToGet = new EventVM()
                        {
                            EventID = reader.GetInt32(0),
                            EventName = reader.GetString(1),
                            EventDescription = reader.GetString(2),
                            EventCreatedDate = reader.GetDateTime(3),
                            LocationID = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4),
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

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/04
        /// 
        /// Description:
        /// Select list of upcoming dates
        /// 
        /// Jace Pettinger
        /// Updated: 2022/02/15
        /// 
        /// Description:
        /// Adding locationID to the returned event objects
        /// 
        /// </summary>
        /// <returns>Event view models</returns>
        public List<EventVM> SelectEventsUpcomingDates()
        {            
            List<EventVM> eventListRef = new List<EventVM>();

            var conn = DBConnection.GetConnection();
            string cmdText = "sp_select_active_events_for_upcoming_dates";
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
                        eventListRef.Add(new EventVM()
                        {
                            EventID = reader.GetInt32(0),
                            EventName = reader.GetString(1),
                            EventDescription = reader.GetString(2),
                            EventCreatedDate = reader.GetDateTime(3),
                            LocationID = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4),
                            EventDates = new List<EventDate>()
                                    {
                                        new EventDate()
                                        {
                                            EventDateID = reader.GetDateTime(5),
                                            EventID = reader.GetInt32(0),
                                            Active = true
                                        }
                                    },
                            Active = true
                        });

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return eventDateVMHelper(eventListRef);

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Select list of upcoming and past dates
        /// 
        /// Jace Pettinger
        /// Updated: 2022/02/15
        /// 
        /// Description:
        /// Adding locationID to the returned event objects
        /// 
        /// </summary>
        /// <returns>Event view models</returns>
        public List<EventVM> SelectEventsUpcomingAndPastDates()
        {
            List<EventVM> eventListRef = new List<EventVM>();

            var conn = DBConnection.GetConnection();
            string cmdText = "sp_select_active_events_for_past_and_future_event_dates";
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
                        eventListRef.Add(new EventVM()
                        {
                            EventID = reader.GetInt32(0),
                            EventName = reader.GetString(1),
                            EventDescription = reader.GetString(2),
                            EventCreatedDate = reader.GetDateTime(3),
                            LocationID = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4),
                            EventDates = new List<EventDate>()
                                    {
                                        new EventDate()
                                        {
                                            EventDateID = reader.GetDateTime(5),
                                            EventID = reader.GetInt32(0),
                                            Active = true
                                        }
                                    },
                            Active = true
                        });

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return eventDateVMHelper(eventListRef);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Select list of past dates
        /// 
        /// Jace Pettinger
        /// Updated: 2022/02/15
        /// 
        /// Description:
        /// Adding locationID to the returned event objects
        /// 
        /// </summary>
        /// <returns>Event view models</returns>
        public List<EventVM> SelectEventsPastDates()
        {
            List<EventVM> eventListRef = new List<EventVM>();

            var conn = DBConnection.GetConnection();
            string cmdText = "sp_select_active_events_for_past_dates";
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
                        eventListRef.Add(new EventVM()
                        {
                            EventID = reader.GetInt32(0),
                            EventName = reader.GetString(1),
                            EventDescription = reader.GetString(2),
                            EventCreatedDate = reader.GetDateTime(3),
                            LocationID = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4),
                            EventDates = new List<EventDate>()
                                    {
                                        new EventDate()
                                        {
                                            EventDateID = reader.GetDateTime(5),
                                            EventID = reader.GetInt32(0),
                                            Active = true
                                        }
                                    },
                            Active = true
                        });

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return eventDateVMHelper(eventListRef);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Select list of upcoming dates for a user
        /// 
        /// Jace Pettinger
        /// Updated: 2022/02/15
        /// 
        /// Description:
        /// Adding locationID to the returned event objects
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>Event view models</returns>
        public List<EventVM> SelectUserEventsForUpcomingDates(int userID)
        {
            List<EventVM> eventListRef = new List<EventVM>();

            var conn = DBConnection.GetConnection();
            string cmdText = "sp_select_active_events_for_upcoming_dates_for_user";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        eventListRef.Add(new EventVM()
                        {
                            EventID = reader.GetInt32(0),
                            EventName = reader.GetString(1),
                            EventDescription = reader.GetString(2),
                            EventCreatedDate = reader.GetDateTime(3),
                            LocationID = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4),
                            EventDates = new List<EventDate>()
                                        {
                                            new EventDate()
                                            {
                                                EventDateID = reader.GetDateTime(5),
                                                EventID = reader.GetInt32(0),
                                                Active = true
                                            }
                                        },
                            Active = true
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return eventDateVMHelper(eventListRef);


        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Select list of past dates for a user
        /// 
        /// Jace Pettinger
        /// Updated: 2022/02/15
        /// 
        /// Description:
        /// Adding locationID to the returned event objects
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>Event view models</returns>
        public List<EventVM> SelectUserEventsForPastDates(int userID)
        {
            List<EventVM> eventListRef = new List<EventVM>();

            var conn = DBConnection.GetConnection();
            string cmdText = "sp_select_active_events_for_past_dates_for_user";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        eventListRef.Add(new EventVM()
                        {
                            EventID = reader.GetInt32(0),
                            EventName = reader.GetString(1),
                            EventDescription = reader.GetString(2),
                            EventCreatedDate = reader.GetDateTime(3),
                            LocationID = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4),
                            EventDates = new List<EventDate>()
                                        {
                                            new EventDate()
                                            {
                                                EventDateID = reader.GetDateTime(5),
                                                EventID = reader.GetInt32(0),
                                                Active = true
                                            }
                                        },
                            Active = true
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return eventDateVMHelper(eventListRef);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Select list of past and upcoming dates for a user
        /// 
        /// Jace Pettinger
        /// Updated: 2022/02/15
        /// 
        /// Description:
        /// Adding locationID to the returned event objects
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>Event view models</returns>
        public List<EventVM> SelectUserEventsForPastAndUpcomingDates(int userID)
        {
            List<EventVM> eventListRef = new List<EventVM>();

            var conn = DBConnection.GetConnection();
            string cmdText = "sp_select_active_events_for_past_and_upcoming_dates_for_user";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        eventListRef.Add(new EventVM()
                        {
                            EventID = reader.GetInt32(0),
                            EventName = reader.GetString(1),
                            EventDescription = reader.GetString(2),
                            EventCreatedDate = reader.GetDateTime(3),
                            LocationID = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4),
                            EventDates = new List<EventDate>()
                                        {
                                            new EventDate()
                                            {
                                                EventDateID = reader.GetDateTime(5),
                                                EventID = reader.GetInt32(0),
                                                Active = true
                                            }
                                        },
                            Active = true
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return eventDateVMHelper(eventListRef);

        }

        /// Christopher Repko
        /// Created: 2022/02/09
        /// 
        /// Description:
        /// Updates the location of an event record
        /// </summary>
        /// <param name="eventID">ID of the event</param>
        /// <param name="oldLocationID">The ID of the old location</param>
        /// <param name="newLocationID">The ID of the new location</param>
        /// <returns>int - rows affected</returns>
        public int UpdateEventLocationByEventID(int eventID, int? oldLocationID, int? newLocationID)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_update_event_location_by_event_id";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EventID", eventID);
            if(oldLocationID is null)
            {
                cmd.Parameters.Add("@OldLocationID", SqlDbType.Int);
                cmd.Parameters["@OldLocationID"].Value = DBNull.Value;
            } else
            {
                cmd.Parameters.Add("@OldLocationID", SqlDbType.Int);
                cmd.Parameters["@OldLocationID"].Value = oldLocationID;
            }
            if(newLocationID is null)
            {

                cmd.Parameters.Add("@LocationID", SqlDbType.Int);
                cmd.Parameters["@LocationID"].Value = DBNull.Value;
            } else
            {
                cmd.Parameters.Add("@LocationID", SqlDbType.Int);
                cmd.Parameters["@LocationID"].Value = newLocationID;
            }

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
        /// Derrick Nagy
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Removes duplicates and adds the dates to the appropriate event
        /// 
        /// Jace Pettinger
        /// Updated: 2022/02/15
        /// 
        /// Description:
        /// Adding locationID variable to the event list
        /// 
        /// </summary>
        /// <param name="eventListRef">Takes an eventvm list</param>
        /// <returns>A list of Events with no duplicate EventIDs and all the EventDates in a list in the Event object</returns>
        private List<EventVM> eventDateVMHelper(List<EventVM> eventListRef)
        {
            List<EventVM> eventList = new List<EventVM>();
            List<EventDate> allDates = new List<EventDate>();
            if (eventListRef.Count > 0)
            {
                foreach (EventVM item in eventListRef)
                {
                    allDates.Add(item.EventDates[0]);

                    eventList.Add(new EventVM()
                    {
                        EventID = item.EventID,
                        EventName = item.EventName,
                        EventDescription = item.EventDescription,
                        EventCreatedDate = item.EventCreatedDate,
                        LocationID = item.LocationID,
                        EventDates = new List<EventDate>()
                    });
                }
            }

            //remove duplicates
            List<EventVM> noDuplicates = eventList.GroupBy(e => e.EventID).Select(e => e.First()).ToList();

            foreach (EventVM item in eventList)
            {
                for (int i = 0; i < allDates.Count; i++)
                {
                    if (item.EventID == allDates[i].EventID)
                    {
                        item.EventDates.Add(allDates[i]);
                    }
                }
            }

            // sort by earliest date
            noDuplicates.Sort((ev1, ev2) => ev1.EventDates[0].EventDateID.CompareTo(ev2.EventDates[0].EventDateID));

            return noDuplicates;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/18
        /// 
        /// Description:
        /// Inserts an event into the database and returns the auto-increment value created for the event id
        /// </summary>
        /// <param name="eventName">The name of the evnet</param>
        /// <param name="eventDescription">The description of the event</param>
        /// <param name="userID">The userID of the user who created the event</param>
        /// <returns></returns>
        public int InsertEventReturnsEventID(string eventName, string eventDescription, int userID)
        {
            int eventID = 0;

            // connection
            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_insert_event_with_user_ID_return_event_id";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EventName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@EventDescription", SqlDbType.NVarChar, 1000);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            cmd.Parameters["@EventName"].Value = eventName;
            cmd.Parameters["@EventDescription"].Value = eventDescription;
            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                Object result = cmd.ExecuteScalar();
                eventID = (int)result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return eventID;
        }
    }
}
