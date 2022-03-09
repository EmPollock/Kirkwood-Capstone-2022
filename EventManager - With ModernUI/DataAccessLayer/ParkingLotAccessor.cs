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
    public class ParkingLotAccessor : IParkingLotAccessor
    {


        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/01
        /// 
        /// Description:
        /// Inserts a parking lot object into the database
        /// </summary>
        /// <param name="parkingLot"></param>
        /// <returns>The inserted parking lot id</returns>
        public int InsertParkingLot(ParkingLot parkingLot)
        {
            int parkingLotID = 0;
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_insert_parking_lot";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@LocationID", SqlDbType.Int);
            cmd.Parameters["@LocationID"].Value = parkingLot.LocationID;

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 160);
            cmd.Parameters["@Name"].Value = parkingLot.Name;

            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 3000);

            if (parkingLot.Description == "" || parkingLot.Description == null)
            {
                cmd.Parameters["@Description"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@Description"].Value = parkingLot.Description;
            }
            
            cmd.Parameters.Add("@ImageName", SqlDbType.NVarChar, 200);

            if (parkingLot.ImageName == null || parkingLot.ImageName == "")
            {
                cmd.Parameters["@ImageName"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@ImageName"].Value = parkingLot.ImageName;
            }

            try
            {
                conn.Open();
                Object result = cmd.ExecuteScalar();
                parkingLotID = (int)result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return parkingLotID;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/01
        /// 
        /// Description:
        /// Selects the list of parking lots for the location ID
        /// </summary>
        /// <param name="locationID">The locationID</param>
        /// <returns>The ParkingLotVMs for the location </returns>
        public List<ParkingLotVM> SelectParkingLotByLocationID(int locationID)
        {
            List<ParkingLotVM> parkingLots = new List<ParkingLotVM>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_parking_lots_by_locationID";

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
                        parkingLots.Add(new ParkingLotVM()
                        {
                            LotID = reader.GetInt32(0),
                            LocationID = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            Description = reader.IsDBNull(3)? null : reader.GetString(3),
                            ImageName = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Active = true,
                            LocationName = reader.GetString(5)
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return parkingLots;

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Deletes parking lot
        /// </summary>
        /// <param name="lotID">The lot to delete</param>
        /// <returns>True is removed, false if not</returns>
        public bool DeleteParkingLotByLotID(int lotID)
        {
            bool isDeleted = false;
            

            // connection
            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_delete_parking_lot";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@LotID", SqlDbType.Int);

            cmd.Parameters["@LotID"].Value = lotID;

            try
            {
                conn.Open();
                isDeleted = (1 == cmd.ExecuteNonQuery());

                try
                {

                }
                catch (Exception ex)
                {
                    throw;
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

            return isDeleted;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Checks to see if the user can edit the parking lot
        /// </summary>
        /// <param name="userID">The ID for the user</param>
        /// <returns>True if removed, false if not</returns>
        public bool UserCanEditParkingLot(int userID)
        {
            bool result = false;
            List<Role> roles = new List<Role>();

            // connection
            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_select_user_roles_from_event_users_table";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            
            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(new Role()
                        {
                            RoleID = reader.GetString(0)
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

            foreach (Role role in roles)
            {
                if (role.RoleID == "Event Planner")
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
    }
    
}
