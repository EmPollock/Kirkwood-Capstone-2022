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
    public class LocationAccessor : ILocationAccessor
    {
        /// <summary>
        /// Kris Howell
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Select all active locations from tadpole_db
        /// 
        /// </summary>
        /// <returns>List of all active locations</returns>
        public List<Location> SelectActiveLocations()
        {
            List<Location> locations = new List<Location>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_active_locations";

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
                        locations.Add(new Location()
                        {
                            LocationID = reader.GetInt32(0),
                            UserID = reader.IsDBNull(1) ? null : (int?)reader.GetInt32(1),
                            Name = reader.GetString(2),
                            Description = reader.IsDBNull(3) ? null : reader.GetString(3),
                            PricingInfo = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Phone = reader.IsDBNull(5) ? null : reader.GetString(5),
                            Email = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Address1 = reader.GetString(7),
                            Address2 = reader.IsDBNull(8) ? null : reader.GetString(8),
                            City = reader.GetString(9),
                            State = reader.GetString(10),
                            ZipCode = reader.GetString(11),
                            ImagePath = reader.IsDBNull(12) ? null : reader.GetString(12),
                            Active = true
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return locations;
        }
    }
}
