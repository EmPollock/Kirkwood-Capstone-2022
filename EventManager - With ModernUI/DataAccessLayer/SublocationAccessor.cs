using System;
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
        public int InsertSublocationByLocationID(int locationID, string sublocationName, string description)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_insert_sublocation_by_locationID";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LocationID", SqlDbType.Int);
            cmd.Parameters["@LocationID"].Value = locationID;
            cmd.Parameters.Add("@SublocationName", SqlDbType.NVarChar, 160);
            cmd.Parameters["@SublocationName"].Value = sublocationName;
            cmd.Parameters.Add("@SublocationDescription", SqlDbType.NVarChar, 1000);
            cmd.Parameters["@SublocationDescription"].Value = description;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally 
            {
                conn.Close();
            }
            return rows;
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Returns a specific Sublocation
        /// 
        /// </summary>
        /// <param name="sublocationID"></param>
        /// <returns>A sublocation object</returns>
        public Sublocation SelectSublocationBySublocationID(int sublocationID)
        {
            Sublocation result = new Sublocation();

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
                        /*	
			                    [SublocationName]
			                    ,[SublocationDescription]
			                    ,[Active]
                                //,[Location ID]	
                        */
                        result = new Sublocation()
                        {
                            SublocationID = sublocationID,
                            SublocationName = reader.GetString(0),
                            SublocationDescription = reader.GetString(1),
                            Active = reader.GetBoolean(2)/*,
                            LocationID = reader.GetInt32(3) */
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

        public List<Sublocation> SelectSublocationsByLocationID(int locationID)
        {
            List<Sublocation> result = new List<Sublocation>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_sublocations_by_locationID";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SublocationID", SqlDbType.Int);

            cmd.Parameters["@SublocationID"].Value = locationID;

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
                            SublocationID = reader.GetInt32(0),
                            SublocationName = reader.GetString(1),
                            SublocationDescription = reader.GetString(2),
                            Active = reader.GetBoolean(3)

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
