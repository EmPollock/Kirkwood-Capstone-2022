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
                            LocationName = reader.GetString(2),
                            LocationDescription = reader.IsDBNull(3) ? null : reader.GetString(3),
                            LocationPricingText = reader.IsDBNull(4) ? null : reader.GetString(4),
                            LocationPhone = reader.IsDBNull(5) ? null : reader.GetString(5),
                            LocationEmail = reader.IsDBNull(6) ? null : reader.GetString(6),
                            LocationAddress1 = reader.GetString(7),
                            LocationAddress2 = reader.IsDBNull(8) ? null : reader.GetString(8),
                            LocationCity = reader.GetString(9),
                            LocationState = reader.GetString(10),
                            LocationZipCode = reader.GetString(11),
                            LocationImagePath = reader.IsDBNull(12) ? null : reader.GetString(12),
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
                            LocationName = reader.GetString(1),
                            LocationDescription = reader.IsDBNull(2) ? null : reader.GetString(2),
                            LocationPricingText = reader.IsDBNull(3) ? null : reader.GetString(3),
                            LocationPhone = reader.IsDBNull(4) ? null : reader.GetString(4),
                            LocationEmail = reader.IsDBNull(5) ? null : reader.GetString(5),
                            LocationAddress1 = reader.GetString(6),
                            LocationAddress2 = reader.IsDBNull(7) ? null : reader.GetString(7),
                            LocationCity = reader.IsDBNull(8) ? null : reader.GetString(8),
                            LocationState = reader.IsDBNull(9) ? null : reader.GetString(9),
                            LocationZipCode = reader.IsDBNull(10) ? null : reader.GetString(10),
                            LocationImagePath = reader.IsDBNull(11) ? null : reader.GetString(11),
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
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Insert event into tadpole_db
        /// 
        /// </summary>
        /// <param name="locationName">Name of the Location</param>
        /// <param name="address">address of the location</param>
        /// <param name="locationCity">address of the location</param>
        /// <param name="locationState">address of the location</param>
        /// <param name="locationZipCode">address of the location</param>
        /// <returns>Number of rows inserted</returns>
        public int InsertLocation(string locationName, string address, string locationCity, string locationState, string locationZipCode)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            string cmdText = "sp_insert_location_by_name_address_city_state_zip";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LocationName", SqlDbType.NVarChar, 160);
            cmd.Parameters.Add("@LocationAddress1", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@locationCity", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@locationState", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@locationZipCode", SqlDbType.NVarChar, 100);

            cmd.Parameters["@LocationName"].Value = locationName;
            cmd.Parameters["@LocationAddress1"].Value = address;
            cmd.Parameters["@locationCity"].Value = locationCity;
            cmd.Parameters["@locationState"].Value = locationState;
            cmd.Parameters["@locationZipCode"].Value = locationZipCode;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                conn.Close();
            }
            return rowsAffected;
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Returns a matching location record
        /// 
        /// </summary>
        /// <param name="locationName">Name of the Location</param>
        /// <param name="address">address of the location</param>
        public Location SelectLocationByLocationNameAndAddress(string locationName, string address)
        {
            Location _matchingLocation = new Location();

            var conn = DBConnection.GetConnection();
            string cmdText = "sp_select_location_by_name_and_address";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LocationName", SqlDbType.NVarChar, 160);
            cmd.Parameters.Add("@LocationAddress1", SqlDbType.NVarChar, 100);

            cmd.Parameters["@LocationName"].Value = locationName;
            cmd.Parameters["@LocationAddress1"].Value = address;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        _matchingLocation = new Location()
                        {
                            LocationID = reader.GetInt32(0),
                            LocationName = reader.GetString(1),
                            LocationDescription = reader.IsDBNull(2) ? null : reader.GetString(2),
                            LocationPricingText = reader.IsDBNull(3) ? null : reader.GetString(3),
                            LocationPhone = reader.IsDBNull(4) ? null : reader.GetString(4),
                            LocationEmail = reader.IsDBNull(5) ? null : reader.GetString(5),
                            LocationAddress1 = reader.GetString(6),
                            Active = true
                        };
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                conn.Close();
            }
            return _matchingLocation;
        }
    }
}
