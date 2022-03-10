using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/03/02
    /// 
    /// The ServiceAccessor data access class for all service data 
    /// </summary>
    public class ServiceAccessor : IServiceAccessor
    {
        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Select all services that match supplier supplierID
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>List of Service objects</returns>
        public List<Service> SelectServicesBySupplierID(int supplierID)
        {
            List<Service> services = new List<Service>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_services_by_supplierID";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
            cmd.Parameters["@SupplierID"].Value = supplierID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        services.Add(new Service()
                        {
                            SupplierID = supplierID,
                            ServiceID = reader.GetInt32(0),
                            ServiceName = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Description = reader.IsDBNull(3) ? null : reader.GetString(3),
                            ServiceImagePath = reader.IsDBNull(4) ? null : reader.GetString(4),
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

            return services;
        }
    }
}
