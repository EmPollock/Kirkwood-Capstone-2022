using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class SupplierAccessor : ISupplierAccessor
    {
        /// <summary>
        /// Kris Howell
        /// Created: 2022/01/27
        /// 
        /// Description:
        /// Select all active suppliers from tadpole_db
        /// 
        /// </summary>
        /// <returns>List of all active suppliers</returns>
        public List<Supplier> SelectActiveSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_active_suppliers";

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
                        suppliers.Add(new Supplier()
                        {
                            SupplierID = reader.GetInt32(0),
                            UserID = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            Description = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Phone = reader.GetString(4),
                            Email = reader.GetString(5),
                            TypeID = reader.GetString(6),
                            Address1 = reader.IsDBNull(7) ? null : reader.GetString(7),
                            Address2 = reader.IsDBNull(8) ? null : reader.GetString(8),
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

            return suppliers;
        }
    }
}
