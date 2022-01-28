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

        public List<string> SelectRolesByEmployeeID(int employeeID)
        {
            List<string> roles = new List<string>();

            var conn = DBConnection.GetConnection();

            // next, we need command text.
            var cmdText = "sp_select_employee_roles_by_employeeID";

            // we create a command object;
            var cmd = new SqlCommand(cmdText, conn);

            // load arguments to the command.
            cmd.CommandType = CommandType.StoredProcedure;

            // We need to add parameters to the command's parameter collection
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);

            // The parameters need their values to be set.
            cmd.Parameters["@EmployeeID"].Value = employeeID;

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

        public User SelectUserByEmail(string email)
        {
            User user = null;

            var conn = DBConnection.GetConnection();

            // next, we need command text.
            var cmdText = "sp_select_employee_by_email";

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
                        currUser.EmployeeID = reader.GetInt32(0);
                        currUser.GivenName = reader.GetString(1);
                        currUser.FamilyName = reader.GetString(2);
                        currUser.PhoneNumber = reader.GetString(3);
                        currUser.EmailAddress = reader.GetString(4);
                        currUser.Active = reader.GetBoolean(5);

                        user = currUser;
                    } 
                } else
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
