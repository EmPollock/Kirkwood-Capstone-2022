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
    public class UserAccessor : IUserAccessor
    {
        /// <summary>
        /// Christopher Repko
        /// Created: 2022/01/21
        /// 
        /// Description:
        /// Retrieves the number of users with a set of credentials
        /// </summary>
        /// <param name="email">The email address to be used as credentials</param>
        /// <param name="passwordHash">The hash of the password to be used as credentials</param>
        /// <returns>The number of matching users</returns>
        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();

            // next, we need command text.
            var cmdText = "sp_authenticate_user";

            // we create a command object;
            var cmd = new SqlCommand(cmdText, conn);

            // load arguments to the command.
            cmd.CommandType = CommandType.StoredProcedure;

            // We need to add parameters to the command's parameter collection
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            // The parameters need their values to be set.
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;


            // Now that we have the command set up, we can execute the command.
            // Always use a try block because the connection is unsafe.
            try
            {
                // Open the connection
                conn.Open();

                // execute appropriately and capture the results.
                result = Convert.ToInt32(cmd.ExecuteScalar());

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
        /// Ramiro Pena
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Creates a new User from the Registration Page
        /// </summary>

        public int InsertUser(User newUser)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_insert_User";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@GivenName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@GivenName"].Value = newUser.GivenName;

            cmd.Parameters.Add("@FamilyName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@FamilyName"].Value = newUser.FamilyName;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters["@Email"].Value = newUser.EmailAddress;

            cmd.Parameters.Add("@UserState", SqlDbType.Char, 2);
            cmd.Parameters["@UserState"].Value = newUser.State;

            cmd.Parameters.Add("@City", SqlDbType.NVarChar, 75);
            cmd.Parameters["@City"].Value = newUser.City;

            cmd.Parameters.Add("@Zip", SqlDbType.Int);
            cmd.Parameters["@Zip"].Value = newUser.Zip;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rows;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/01/21
        /// 
        /// Description:
        /// Selects roles of a user. Not currently fully implemented. Legacy code left as a starting point for whoever implements it.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<string> SelectRolesByUserID(int userID)
        {
            List<string> roles = new List<string>();

            var conn = DBConnection.GetConnection();

            // next, we need command text.
            var cmdText = "sp_select_user_roles_by_userID";

            // we create a command object;
            var cmd = new SqlCommand(cmdText, conn);

            // load arguments to the command.
            cmd.CommandType = CommandType.StoredProcedure;

            // We need to add parameters to the command's parameter collection
            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            // The parameters need their values to be set.
            cmd.Parameters["@UserID"].Value = userID;

            // Now that we have the command set up, we can execute the command.
            // Always use a try block because the connection is unsafe.
            try
            {
                // Open the connection
                conn.Open();

                // execute appropriately and capture the results.
                var reader = cmd.ExecuteReader();

                // Process results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
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


            return roles;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/01/21
        /// 
        /// Description:
        /// Method to retrieve user data using an email address.
        /// </summary>
        /// <param name="email">Email of user to be retrieved</param>
        /// <exception cref="ApplicationException">No user found for email</exception>
        /// <returns>User object containing information about the found user.</returns>
        public User SelectUserByEmail(string email)
        {
            User user = null;

            var conn = DBConnection.GetConnection();

            // next, we need command text.
            var cmdText = "sp_select_user_by_email";

            // we create a command object;
            var cmd = new SqlCommand(cmdText, conn);

            // load arguments to the command.
            cmd.CommandType = CommandType.StoredProcedure;

            // We need to add parameters to the command's parameter collection
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);

            // The parameters need their values to be set.
            cmd.Parameters["@Email"].Value = email;

            // Now that we have the command set up, we can execute the command.
            // Always use a try block because the connection is unsafe.
            try
            {
                // Open the connection
                conn.Open();

                // execute appropriately and capture the results.
                var reader = cmd.ExecuteReader();

                // Process results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        User currUser = new User();
                        currUser.UserID = reader.GetInt32(0);
                        currUser.GivenName = reader.GetString(1);
                        currUser.FamilyName = reader.GetString(2);
                        currUser.EmailAddress = reader.GetString(3);
                        currUser.City = reader.GetString(5);
                        currUser.Zip = reader.GetInt32(6);
                        currUser.Active = reader.GetBoolean(7);

                        user = currUser;
                    }
                }
                else
                {
                    throw new ApplicationException("User not found.");
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

            return user;
        }
        /// <summary>
        /// Christopher Repko
        /// Created: 2022/01/21
        /// 
        /// Description:
        /// Method to update password hash
        /// </summary>
        /// <param name="email">Email address of user</param>
        /// <param name="oldPasswordHash">The old hash to be replaced</param>
        /// <param name="newPasswordHash">The new hash to replace the old hash with.</param>
        /// <returns>number of update operations that go through</returns>
        public int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();

            // next, we need command text.
            var cmdText = "sp_update_passwordHash";

            // we create a command object;
            var cmd = new SqlCommand(cmdText, conn);

            // load arguments to the command.
            cmd.CommandType = CommandType.StoredProcedure;

            // We need to add parameters to the command's parameter collection
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);

            // The parameters need their values to be set.
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@OldPasswordHash"].Value = oldPasswordHash;
            cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;

            // Now that we have the command set up, we can execute the command.
            // Always use a try block because the connection is unsafe.
            try
            {
                // Open the connection
                conn.Open();

                // execute appropriately and capture the results.
                rowsAffected = cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }
    }
}
