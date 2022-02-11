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

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// Function to take a supplier ID and get a list of images from the database
        /// </summary>
        /// <param name="supplierID">Supplier ID to use to search for images</param>
        /// <returns>A list of images for the supplier ID</returns>
        public List<string> SelectSupplierImagesBySupplierID(int supplierID)
        {
            List<string> result = new List<string>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_supplier_tags";

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
                        result.Add(reader.GetString(0));
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

            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// Function to take a supplier ID and get a list of reviews from the database
        /// </summary>
        /// <param name="supplierID">Supplier ID to use to search for reviews</param>
        /// <returns>A list of reviews for the supplier ID</returns>
        public List<Reviews> SelectSupplierReviewsBySupplierID(int supplierID)
        {
            List<Reviews> result = new List<Reviews>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_supplier_reviews";

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
                        result.Add(new Reviews()
                        {
                            ReviewID = reader.GetInt32(0),
                            FullName = reader.GetString(1),
                            ReviewType = reader.GetString(2),
                            Rating = reader.GetInt32(3),
                            Review = reader.GetString(4),
                            DateCreated = reader.GetDateTime(5),
                            Active = reader.GetBoolean(6),
                            ForeignID = supplierID
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

            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// Function to take a supplier ID and get a list of tags from the database
        /// </summary>
        /// <param name="supplierID">Supplier ID to use to search for tags</param>
        /// <returns>A list of tags for the supplier ID</returns>
        public List<string> SelectSupplierTagsBySupplierID(int supplierID)
        {
            List<string> result = new List<string>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_supplier_images";

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
                        result.Add(reader.GetString(0));
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

            return result;
        }
    }
}
