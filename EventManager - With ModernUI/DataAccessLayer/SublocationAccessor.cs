﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessLayer
{
    public class SublocationAccessor : ISublocationAccessor
    {
      
        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Returns a specific Sublocation
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/24
        /// 
        /// Updated to remove default sublocation. Also reenabled the location ID
        /// </summary>
        /// <param name="sublocationID"></param>
        /// <returns>A sublocation object</returns>
        public Sublocation SelectSublocationBySublocationID(int sublocationID)
        {
            Sublocation result = null;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_sublocation_by_sublocationID";

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
                        result = new Sublocation()
                        {
                            SublocationID = sublocationID,
                            SublocationName = reader.GetString(0),
                            SublocationDescription = reader.GetString(1),
                            Active = reader.GetBoolean(2),
                            LocationID = reader.GetInt32(3)
                        };
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
        /// Christopher Repko
        /// Created 2022/02/24
        /// 
        /// Description:
        /// Returns a list of sublocations based on a locationID
        /// </summary>
        /// <param name="locationID">The locationID to get sublocations matching.</param>
        /// <returns>A list of sublocations matching the location ID passed in.</returns>
        public List<Sublocation> SelectSublocationsByLocationID(int locationID)
        {
            List<Sublocation> result = new List<Sublocation>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_sublocations_by_locationID";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@LocationID", SqlDbType.Int);

            cmd.Parameters["@LocationID"].Value = locationID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new Sublocation()
                        {
                            LocationID = locationID,
                            SublocationName = reader.GetString(0),
                            SublocationDescription = reader.GetString(1),
                            Active = reader.GetBoolean(2),
                            SublocationID = reader.GetInt32(3)
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
