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
    /// Kris Howell
    /// Created: 2022/02/03
    /// 
    /// The LocationAccessor data access class for all location data 
    /// </summary>
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

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Accessor method that that selects the location matching the locationID provided and returns a location 
        /// data object. Will be null if no location is found
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <param name="locationID"></param>
        /// <returns>A Location object</returns>
        public Location SelectLocationByLocationID(int locationID)
        {
            Location location = new Location();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_location_by_locationID";

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
                        location = new Location()
                        {
                            LocationID = locationID,
                            UserID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                            PricingInfo = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Phone = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Email = reader.IsDBNull(5) ? null : reader.GetString(5),
                            Address1 = reader.GetString(6),
                            Address2 = reader.IsDBNull(7) ? null : reader.GetString(7),
                            City = reader.IsDBNull(8) ? null : reader.GetString(8),
                            State = reader.IsDBNull(9) ? null : reader.GetString(9),
                            ZipCode = reader.IsDBNull(10) ? null : reader.GetString(10),
                            ImagePath = reader.IsDBNull(11) ? null : reader.GetString(11),
                            Active = reader.GetBoolean(12)
                        };
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

            return location;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// Accessor method that that selects the location images matching the locationID provided and returns a list of location image
        /// data objects. Will be null if no location images are found
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <param name="locationID"></param>
        /// <returns>A List of LocationImage objects</returns>
        public List<LocationImage> SelectLocationImages(int locationID)
        {
            List<LocationImage> locationImages = new List<LocationImage>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_location_images";

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
                        locationImages.Add(new LocationImage()
                        {
                            LocationID = locationID,
                            ImageName = reader.GetString(0)
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

            return locationImages;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/04
        /// 
        /// Description:
        /// Accessor method that that selects the location reviews matching the locationID provided and returns a list of location review
        /// data objects. Will be null if no location reviews are found
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        /// <param name="locationID"></param>
        /// <returns>A List of LocationReview objects</returns>
        public List<LocationReview> SelectLocationReviews(int locationID)
        {
            List<LocationReview> locationReviews = new List<LocationReview>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_location_reviews";

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
                        locationReviews.Add(new LocationReview()
                        {
                            LocationID = locationID,
                            ReviewID = reader.GetInt32(0),
                            FullName = reader.GetString(1),
                            ReviewType = reader.GetString(2),
                            Rating = reader.GetInt32(3),
                            Review = reader.IsDBNull(4) ? null : reader.GetString(4),
                            DateCreated = reader.GetDateTime(5),
                            Active = reader.GetBoolean(6)
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

            return locationReviews;
        }
    }
}