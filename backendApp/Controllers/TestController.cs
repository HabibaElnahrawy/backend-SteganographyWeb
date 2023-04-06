using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using backendApp.Models;

namespace backendApp.Controllers
{
    [RoutePrefix("api/Test")]
    public class TestController : ApiController
    {
        SqlConnection conn = new SqlConnection("Server = localhost; Database=master; Integrated Security = True;");
        SqlCommand cmd = null;
        SqlDataAdapter da = null;


        [HttpPost]
        [Route("Registration")]
        public string Registration(User user)
        {

            string msg = string.Empty;
            try
            {
                cmd = new SqlCommand("usp_Registration", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@PhoneNo", user.PhoneNo);
                cmd.Parameters.AddWithValue("@Address", user.Address);
                cmd.Parameters.AddWithValue("@IsActive", user.IsActive);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();

                if (i > 0)
                {
                    msg = "Data inserted ";
                }
                else
                {
                    msg = "error";
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
           
            return msg;

        }




        [HttpPost]
        [Route("Login")]
        public string Login(User user)
        {

            string msg = string.Empty;
            try
            {
                da = new SqlDataAdapter("usp_Login", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Name", user.Name);
                da.SelectCommand.Parameters.AddWithValue("@PhoneNo", user.PhoneNo);
                DataTable dt = new DataTable();

                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    msg = "user is valid";

                }
                else
                {
                    msg = "user is Invalid";
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return msg;

        }
    }
}
