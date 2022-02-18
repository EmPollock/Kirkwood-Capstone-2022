/// <summary>
/// Vinayak Deshpande
/// 2022/01/26
/// 
/// Volunteer Request Accessor
/// </summary>
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;


namespace DataAccessLayer
{
    public class VolunteerRequestAccessor : IVolunteerRequestAccessor
    {
        public List<VolunteerRequest> SelectVolunteerRequestsByEventID(int eventID)
        {
            List<VolunteerRequest> requests = new List<VolunteerRequest>();

            var conn = DBConnection.GetConnection();

            // next, we need command text.
            var cmdText = "sp_select_all_requests_for_event_by_eventID";

            // we create a command object;
            var cmd = new SqlCommand(cmdText, conn);

            // load arguments to the command.
            cmd.CommandType = CommandType.StoredProcedure;

            // We need to add parameters to the command's parameter collection
            cmd.Parameters.Add("@EventID", SqlDbType.Int);

            // The parameters need their values to be set.
            cmd.Parameters["@EventID"].Value = eventID;

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
                        var tempRequest = new VolunteerRequest();

                        tempRequest.RequestID = reader.GetInt32(0);
                        tempRequest.VolunteerID = reader.GetInt32(1);
                        tempRequest.TaskID = reader.GetInt32(2);
                        tempRequest.VolunteerResponse = HelperMethods.IsNullCheck(reader, 3);
                        tempRequest.EventResponse = HelperMethods.IsNullCheck(reader, 4);

                        requests.Add(tempRequest);

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


            return requests;
        }
    }
}
