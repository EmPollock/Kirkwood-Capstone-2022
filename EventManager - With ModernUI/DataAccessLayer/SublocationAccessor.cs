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

        
    }
}
