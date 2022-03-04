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
    public class EntranceAccessor : IEntranceAccessor
    {

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/04
        /// 
        /// Description:
        /// Select that returns entrances based off a location ID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public List<Entrance> SelectEntranceByLocationID(int locationID)
        {
            List<Entrance> entrances = new List<Entrance>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_entrance_by_locationID";
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
                    while(reader.Read())
                    {
                        entrances.Add(new Entrance()
                        {
                            EntranceID = reader.GetInt32(0),
                            LocationID = reader.GetInt32(1),
                            EntranceName = reader.GetString(2),
                            Description = reader.GetString(3)
                        });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return entrances;
        }
    }
}
