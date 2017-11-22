using GMC_Consignment_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GMC_Consignment_API.Controllers
{
    [RoutePrefix("api/User")]
    [EnableCors("*", "*", "*")]
    public class UserController : ApiController
    {
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="creds">The credentials of the user being registered.</param>
        /// <returns>The success of the registration.</returns>
        [HttpPost]
        [Route("RegisterUser")]
        public int RegisterUser(UserCredentials creds)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_addUser");
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Username", creds.Username));
            command.Parameters.Add(new SqlParameter("@Password", creds.Password));
            command.Parameters.Add(new SqlParameter("@SKUMin", creds.SKUMin));
            command.Parameters.Add(new SqlParameter("@SKUMax", creds.SKUMax));
            command.Parameters.Add(new SqlParameter("@Name", creds.Name));
            command.Parameters.Add(new SqlParameter("@Email", creds.Email));

            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            if (table.Rows.Count > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Removes a user from the database.
        /// </summary>
        /// <param name="userID">The ID number of the user being removed.</param>
        /// <returns>The success of the removal.</returns>
        [HttpPost]
        [Route("RemoveUser")]
        public int RemoveUser(int userID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_removeUser");
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@UserID", userID));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            if (table.Rows.Count == 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        
        /// <summary>
        /// Changes the username of a given user.
        /// </summary>
        /// <param name="newName">The new name of the user.</param>
        /// <param name="userID">The ID number of the user whose username is being changed.</param>
        /// <returns>The success of the change.</returns>
        [HttpPost]
        [Route("ChangeUsername")]
        public int ChangeUsername(string newName, int userID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_changeUsername");
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@UserID", userID));
            command.Parameters.Add(new SqlParameter("@newUsername", newName));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            if (table.Rows[0][0].ToString() == newName)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Changes the password of a given user.
        /// </summary>
        /// <param name="newPassword">The new password for the user.</param>
        /// <param name="userID">The ID number of the user whose password is being changed.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ChangePassword")]
        public int ChangePassword(string newPassword, int userID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_changePassword");
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@UserID", userID));
            command.Parameters.Add(new SqlParameter("@newPassword", newPassword));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            if (table.Rows[0][0].ToString() == newPassword)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Changes the SKU range of a given user.
        /// </summary>
        /// <param name="newMin">The new minimum SKU of the user.</param>
        /// <param name="newMax">The new maximum SKU of the user.</param>
        /// <param name="userID">The ID number of the user whose SKU range is being changed.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ChangeSKURange")]
        public int ChangeSKURange(int newMin, int newMax, int userID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_changeSKURange");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Min", newMin));
            command.Parameters.Add(new SqlParameter("@Max", newMax));
            command.Parameters.Add(new SqlParameter("@ID", userID));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            if (int.Parse(table.Rows[0][0].ToString()) == newMin && int.Parse(table.Rows[0][1].ToString()) == newMax)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Changes the full name of a given user.
        /// </summary>
        /// <param name="newName">The new name of the user.</param>
        /// <param name="userID">The ID number of the user whose name is being changed.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ChangeName")]
        public int ChangeName(string newName, int userID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_changeName");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@NewName", newName));
            command.Parameters.Add(new SqlParameter("@UserID", userID));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            if (table.Rows[0][0].ToString() == newName)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Changes the registered email address of a given user.
        /// </summary>
        /// <param name="newEmail">The new email address for the user.</param>
        /// <param name="userID">The ID number of the user whose email is being changed.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ChangeEmail")]
        public int ChangeEmail(string newEmail, int userID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_changeEmail");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@NewEmail", newEmail));
            command.Parameters.Add(new SqlParameter("@UserID", userID));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            if (table.Rows[0][0].ToString() == newEmail)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Returns a user's username.
        /// </summary>
        /// <param name="userID">The ID number of the user whose username has been requested.</param>
        /// <returns>The user's username.</returns>
        [HttpGet]
        [Route("GetUsernameByID")]
        public string GetUsernameByID(int userID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_IDtoUsername");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@UserID", userID));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            return table.Rows[0][0].ToString();
        }

        [HttpGet]
        [Route("GetIDByUsername")]
        public string GetIDByUsername(string username)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_usernameToID");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Username", username));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            return table.Rows[0][0].ToString();
        }

        [HttpGet]
        [Route("GetPasswordByID")]
        public string GetPasswordByID(int userID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getPasswordByID");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@UserID", userID));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            return table.Rows[0][0].ToString();
        }

        [HttpGet]
        [Route("GetSKURangeByID")]
        public string GetSKURangeByID(int userID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getSKURangeByID");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@UserID", userID));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            return table.Rows[0][0].ToString() + "," + table.Rows[0][1].ToString();
        }

        [HttpGet]
        [Route("GetNameByID")]
        public string GetNameByID(int userID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getNameFromID");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@UserID", userID));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            return table.Rows[0][0].ToString();
        }

        [HttpGet]
        [Route("GetEmailByID")]
        public string GetEmailByID(int userID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getEmailFromID");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@UserID", userID));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            return table.Rows[0][0].ToString();
        }
    }
}
