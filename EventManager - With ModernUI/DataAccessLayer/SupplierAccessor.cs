﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

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
        /// Kris Howell
        /// Updated: 2022/02/18
        /// 
        /// Description:
        /// Add City, State, ZipCode to match sp
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
                            Address1 = reader.GetString(7),
                            Address2 = reader.IsDBNull(8) ? null : reader.GetString(8),
                            City = reader.GetString(9),
                            State = reader.GetString(10),
                            ZipCode = reader.GetString(11),
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
        /// Kris Howell
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Select regular weekly availability records matching the given supplierID and date.
        /// 
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Added exception
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <param name="date"></param>
        /// <returns>A list of availability objects for a Supplier on a given date</returns>
        public List<Availability> SelectSupplierAvailabilityBySupplierIDAndDate(int supplierID, DateTime date)
        {
            List<Availability> supplierAvailabilities = new List<Availability>();
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_availability_by_supplierID_and_date";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
            cmd.Parameters["@SupplierID"].Value = supplierID;
            cmd.Parameters.Add("@AvailabilityDate", SqlDbType.Date);
            cmd.Parameters["@AvailabilityDate"].Value = date;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        supplierAvailabilities.Add(new Availability()
                        {
                            ForeignID = supplierID,
                            AvailabilityID = reader.GetInt32(0),
                            TimeStart = DateTime.ParseExact(reader["TimeStart"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            TimeEnd = DateTime.ParseExact(reader["TimeEnd"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture)
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

            return supplierAvailabilities;
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Select one-off availability exception records matching the given supplierID and date.
        /// </summary>
        /// <param name="supplierID"></param>
        /// <param name="date"></param>
        /// <returns>A list of availability objects for a Supplier on a given date</returns>
        public List<Availability> SelectSupplierAvailabilityExceptionBySupplierIDAndDate(int supplierID, DateTime date)
        {
            List<Availability> supplierAvailabilities = new List<Availability>();
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_availability_exception_by_supplierID_and_date";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
            cmd.Parameters["@SupplierID"].Value = supplierID;
            cmd.Parameters.Add("@ExceptionDate", SqlDbType.Date);
            cmd.Parameters["@ExceptionDate"].Value = date;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.IsDBNull(1))
                        {
                            // null start time in DB means user made exception to have no availability on this date
                            // return blank availability object so that no availability is displayed on this date per the exception
                            return new List<Availability>(){
                                new Availability()
                                {
                                    ForeignID = supplierID,
                                    AvailabilityID = reader.GetInt32(0)
                                }
                            };
                        }

                        supplierAvailabilities.Add(new Availability()
                        {
                            ForeignID = supplierID,
                            AvailabilityID = reader.GetInt32(0),
                            TimeStart = DateTime.ParseExact(reader["TimeStart"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            TimeEnd = DateTime.ParseExact(reader["TimeEnd"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture)
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

            return supplierAvailabilities;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// Select availability records matching the given supplierID 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>A list of availability objects for a Supplier</returns>
        public List<AvailabilityVM> SelectSupplierAvailabilityBySupplierID(int supplierID)
        {
            List<AvailabilityVM> supplierAvailabilities = new List<AvailabilityVM>();
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_availability_by_supplierID";

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
                        supplierAvailabilities.Add(new AvailabilityVM()
                        {
                            ForeignID = supplierID,
                            AvailabilityID = reader.GetInt32(0),
                            TimeStart = DateTime.ParseExact(reader["TimeStart"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            TimeEnd = DateTime.ParseExact(reader["TimeEnd"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            Sunday = reader.GetBoolean(3),
                            Monday = reader.GetBoolean(4),
                            Tuesday = reader.GetBoolean(5),
                            Wednesday = reader.GetBoolean(6),
                            Thursday = reader.GetBoolean(7),
                            Friday = reader.GetBoolean(8),
                            Saturday = reader.GetBoolean(9)
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

            return supplierAvailabilities;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// Select availability exception records matching the given supplierID
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>A list of availability objects for a Supplier</returns>
        public List<Availability> SelectSupplierAvailabilityExceptionBySupplierID(int supplierID)
        {
            List<Availability> supplierAvailabilities = new List<Availability>();
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_availability_exception_by_supplierID";

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
                        supplierAvailabilities.Add(new Availability()
                        {
                            ForeignID = supplierID,
                            AvailabilityID = reader.GetInt32(0),
                            DateID = reader.GetDateTime(1),
                            TimeStart = reader.IsDBNull(2) ? DateTime.MinValue : DateTime.ParseExact(reader["TimeStart"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            TimeEnd = reader.IsDBNull(3) ? DateTime.MinValue : DateTime.ParseExact(reader["TimeEnd"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture)
                        });
                        if(supplierAvailabilities.Last().TimeStart == DateTime.MinValue && supplierAvailabilities.Last().TimeEnd == DateTime.MinValue)
                        {
                            supplierAvailabilities.Last().TimeStart = null;
                            supplierAvailabilities.Last().TimeEnd = null;
                        }
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

            return supplierAvailabilities;
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/04
        /// 
        /// Description:
        /// Selects a supplier from the supplier table.
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>A supplier with the given supplierId</returns>
        public Supplier SelectSupplierBySupplierID(int supplierID)
        {
            Supplier supplier = null;
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_supplier_by_supplierID";
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
                        supplier = new Supplier()
                        {
                            SupplierID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Phone = reader.GetString(3),
                            Email = reader.GetString(4),
                            TypeID = reader.GetString(5),
                            Address1 = reader.GetString(6),
                            City = reader.GetString(7),
                            State = reader.GetString(8),
                            Active = true
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return supplier;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Accessor that returns a list of dates that the supplier has available for the next three months
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public List<DateTime> SelectSupplierAvailabilityForNextThreeMonths(int supplierID)
        {
            List<DateTime> threeMonthAvailability = new List<DateTime>();
            List<Availability> weeklyAvailability = new List<Availability>();
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_availability_by_supplierID";

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
                        weeklyAvailability.Add(new Availability()
                        {
                            ForeignID = supplierID,
                            AvailabilityID = reader.GetInt32(0),
                            TimeStart = DateTime.ParseExact(reader["TimeStart"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            TimeEnd = DateTime.ParseExact(reader["TimeEnd"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            Sunday = reader.GetBoolean(3),
                            Monday = reader.GetBoolean(4),
                            Tuesday = reader.GetBoolean(5),
                            Wednesday = reader.GetBoolean(6),
                            Thursday = reader.GetBoolean(7),
                            Friday = reader.GetBoolean(8),
                            Saturday = reader.GetBoolean(9)
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

            foreach (Availability availability in weeklyAvailability)
            {
                DateTime timeStart = (DateTime)availability.TimeStart;
                // get about 13 weeks of dates, drop any that happen before or after
                for (int i = 0; i < 92; i += 7)
                {
                    int currentDayOfWeek = (int)DateTime.Now.DayOfWeek;

                    if (availability.Sunday)
                    {
                        // target day - current day
                        int diff = 0 - currentDayOfWeek;
                        DateTime date = DateTime.Today.AddDays(diff + i);
                        threeMonthAvailability.Add(date);
                    }
                    if (availability.Monday)
                    {
                        int diff = 1 - currentDayOfWeek;
                        DateTime date = DateTime.Today.AddDays(diff + i);
                        threeMonthAvailability.Add(date);
                    }
                    if (availability.Tuesday)
                    {
                        int diff = 2 - currentDayOfWeek;
                        DateTime date = DateTime.Today.AddDays(diff + i);
                        threeMonthAvailability.Add(date);
                    }
                    if (availability.Wednesday)
                    {
                        int diff = 3 - currentDayOfWeek;
                        DateTime date = DateTime.Today.AddDays(diff + i);
                        threeMonthAvailability.Add(date);
                    }
                    if (availability.Thursday)
                    {
                        int diff = 4 - currentDayOfWeek;
                        DateTime date = DateTime.Today.AddDays(diff + i);
                        threeMonthAvailability.Add(date);
                    }
                    if (availability.Friday)
                    {
                        int diff = 5 - currentDayOfWeek;
                        DateTime date = DateTime.Today.AddDays(diff + i);
                        threeMonthAvailability.Add(date);
                    }
                    if (availability.Saturday)
                    {
                        int diff = 6 - currentDayOfWeek;
                        DateTime date = DateTime.Today.AddDays(diff + i);
                        threeMonthAvailability.Add(date);
                    }
                }
            }

            // remove unwanted dates


            return threeMonthAvailability;
        }
    }
}
