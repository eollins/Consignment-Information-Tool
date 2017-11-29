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
        [HttpPost]
        [Route("AddUser")]
        public int AddUser(UserCredentials creds)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_addUser");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Username", creds.Username));
            command.Parameters.Add(new SqlParameter("@Password", creds.Password));
            command.Parameters.Add(new SqlParameter("@Email", creds.Email));
            command.Parameters.Add(new SqlParameter("@Name", creds.Name));
            command.Parameters.Add(new SqlParameter("@IsTest", creds.IsTest));
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

        [HttpPost]
        [Route("AddConsignment")]
        public int AddConsignment(ConsignmentInformation info)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_addConsignment");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@SKUMin", info.SKUMin));
            command.Parameters.Add(new SqlParameter("@SKUMax", info.SKUMax));
            command.Parameters.Add(new SqlParameter("@ConsignmentName", info.ConsignmentName));
            command.Parameters.Add(new SqlParameter("@Total", info.Total));
            command.Parameters.Add(new SqlParameter("@IsTest", info.IsTest));
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

        [HttpPost]
        [Route("AssignConsignment")]
        public int AssignConsignment(Assignment assignment)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_assignConsignment");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@UserID", assignment.UserID));
            command.Parameters.Add(new SqlParameter("@ConsignmentID", assignment.ConsignmentID));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            if (table.Rows[0][0].ToString() == assignment.UserID.ToString() && table.Rows[0][1].ToString() == assignment.ConsignmentID.ToString())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        [HttpPost]
        [Route("UnassignConsignment")]
        public int UnassignConsignment(Assignment assignment)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_unassignConsignment");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@UserID", assignment.UserID));
            command.Parameters.Add(new SqlParameter("@ConsignmentID", assignment.ConsignmentID));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            if (table.Rows[0][0].ToString() != assignment.UserID.ToString() && table.Rows[0][1].ToString() != assignment.ConsignmentID.ToString())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        [HttpPost]
        [Route("RemoveUser")]
        public int RemoveUser(int userID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_removeUser");
            command.CommandType = CommandType.StoredProcedure;
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

        [HttpPost]
        [Route("RemoveConsignment")]
        public int RemoveConsignment(int userID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_removeConsignment");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ConsignmentID", userID));
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

        [HttpPost]
        [Route("ChangeUsername")]
        public int ChangeUsername(int userID, string newUsername)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_changeUsername");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@UserID", userID));
            command.Parameters.Add(new SqlParameter("@NewUsername", newUsername));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            if (table.Rows[0][0].ToString() == newUsername)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        [HttpPost]
        [Route("ChangePassword")]
        public int ChangePassword(int userID, string newPassword)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_changePassword");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@UserID", userID));
            command.Parameters.Add(new SqlParameter("@NewPassword", newPassword));
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

        [HttpPost]
        [Route("ChangeEmail")]
        public int ChangeEmail(int userID, string newEmail)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_changeEmail");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@UserID", userID));
            command.Parameters.Add(new SqlParameter("@NewEmail", newEmail));
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

        [HttpPost]
        [Route("ChangeName")]
        public int ChangeName(int userID, string newName)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_changeName");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@UserID", userID));
            command.Parameters.Add(new SqlParameter("@NewName", newName));
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

        [HttpPost]
        [Route("ChangeSKURange")]
        public int ChangeSKURange(SKURange range)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_changeSKURange");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ConsignmentID", range.ConsignmentID));
            command.Parameters.Add(new SqlParameter("@SKUMin", range.Min));
            command.Parameters.Add(new SqlParameter("@SKUMax", range.Max));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            if (table.Rows[0][0].ToString() == range.Min.ToString() && table.Rows[0][1].ToString() == range.Max.ToString())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        [HttpPost]
        [Route("ChangeConsignmentName")]
        public int ChangeConsignmentName(int consignmentID, string newName)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_changeConsignmentName");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ConsignmentID", consignmentID));
            command.Parameters.Add(new SqlParameter("@NewName", newName));
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

        [HttpPost]
        [Route("ChangeConsignmentStatus")]
        public int ChangeConsignmentStatus(int consignmentID, int newStatus)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_changeConsignmentStatus");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ConsignmentID", consignmentID));
            command.Parameters.Add(new SqlParameter("@NewStatus", newStatus));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            if (table.Rows[0][0].ToString() == newStatus.ToString())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        [HttpPost]
        [Route("ChangeTotal")]
        public int ChangeTotal(int consignmenID, string newTotal)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_changeTotal");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ConsignmentID", consignmenID));
            command.Parameters.Add(new SqlParameter("@NewTotal", newTotal));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            if (table.Rows[0][0].ToString() == newTotal)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        [HttpPost]
        [Route("ChangeMoneyMade")]
        public int ChangeMoneyMade(int consignmentID, string moneyMade)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_changeMoneyMade");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ConsignmentID", consignmentID));
            command.Parameters.Add(new SqlParameter("@NewAmount", moneyMade));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            if (table.Rows[0][0].ToString() == moneyMade)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        [HttpGet]
        [Route("GetUsername")]
        public string GetUsername(int userID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getUsername");
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
        [Route("GetPassword")]
        public string GetPassword(int userID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getPassword");
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
        [Route("GetEmail")]
        public string GetEmail(int userID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getEmail");
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
        [Route("GetName")]
        public string GetName(int userID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getName");
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
        [Route("GetConsignment")]
        public string GetConsignment(int userID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getConsignment");
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
        [Route("GetUserID")]
        public string GetUserID(string username)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getUserID");
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
        [Route("Authenticate")]
        public int Authenticate(LoginInfo info)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_Authenticate");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Username", info.Username));
            command.Parameters.Add(new SqlParameter("@Password", info.Password));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            if (table.Rows[0][0].ToString() == info.Username && table.Rows[0][1].ToString() == info.Password)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        [HttpGet]
        [Route("GetUser")]
        public string GetUser(int consignmentID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getUser");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ConsignmentID", consignmentID));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            return table.Rows[0][0].ToString();
        }

        [HttpGet]
        [Route("GetSKURange")]
        public string GetSKURange(int consignmentID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getSKURange");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ConsignmentID", consignmentID));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            return table.Rows[0][0].ToString() + "," + table.Rows[0][1].ToString();
        }

        [HttpGet]
        [Route("GetConsignmentID")]
        public string GetConsignmentID(string consignmentName)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getConsignmentID");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ConsignmentName", consignmentName));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            return table.Rows[0][0].ToString();
        }

        [HttpGet]
        [Route("GetConsignmentName")]
        public string GetConsignmentName(int consignmentID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getConsignmentName");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ConsignmentID", consignmentID));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            return table.Rows[0][0].ToString();
        }

        [HttpGet]
        [Route("GetConsignmentStatus")]
        public int GetConsignmentStatus(int consignmentID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getConsignmentStatus");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ConsignmentID", consignmentID));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            return int.Parse(table.Rows[0][0].ToString());
        }

        [HttpGet]
        [Route("GetTotal")]
        public string GetTotal(int consignmentID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getTotal");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ConsignmentID", consignmentID));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            return table.Rows[0][0].ToString();
        }

        [HttpGet]
        [Route("GetMoneyMade")]
        public string GetMoneyMade(int consignmentID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getMoneyMade");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ConsignmentID", consignmentID));
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
