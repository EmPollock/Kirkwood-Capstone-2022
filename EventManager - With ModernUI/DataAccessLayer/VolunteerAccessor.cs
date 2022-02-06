﻿using System;
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
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/01/26
    /// 
    /// The VolunteerAccessor data access class for all volunteer data 
    /// </summary>
    public class VolunteerAccessor : IVolunteerAccessor
    {
        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Accessor method that that selects all volunteer reviews and returns a list of them in the form of 
        /// a volunteer data object
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <returns>A list of volunteer data object shells that records the Volunteer ID and rating</returns>
        public List<Volunteer> SelectAllVolunteerReviews()
        {
            List<Volunteer> volunteers = new List<Volunteer>();

            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_select_all_volunteer_reviews";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        volunteers.Add(new Volunteer()
                        {
                            VolunteerID = reader.GetInt32(0),
                            Rating = reader.GetInt32(1)
                        });
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

            return volunteers;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Accessor method that that selects all volunteers and returns a list of them in the form of 
        /// a volunteer data object
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <returns>A list volunteer data object</returns>
        public List<Volunteer> SelectAllVolunteers()
        {
            List<Volunteer> volunteers = new List<Volunteer>();

            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_select_all_volunteers";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        volunteers.Add(new Volunteer()
                        {
                            VolunteerID = reader.GetInt32(0),
                            GivenName = reader.GetString(1),
                            FamilyName = reader.GetString(2),
                            State = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            City = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            Zip = reader.IsDBNull(5) ? 0 : reader.GetInt32(5),
                            VolunteerType = reader.GetString(6)
                        });
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

            return volunteers;
        }
    }
}