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
using System.Xml;

namespace GMC_Consignment_API.Controllers
{
    [RoutePrefix("api/Item")]
    [EnableCors("*", "*", "*")]
    public class ItemController : ApiController
    {
        [HttpPost]
        [Route("AddItem")]
        public int AddItem(ItemData data)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_addItem");
            command.CommandType = System.Data.CommandType.StoredProcedure;

            if (data.IsTest == "0")
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("http://open.api.ebay.com/shopping?callname=GetSingleItem&responseencoding=XML&appid=GregoryM-mailer-PRD-a45ed6035-97c14545&siteid=0&version=967&ItemID=" + data.ItemNumber);
                string title;

                try
                {
                    title = ((XmlElement)doc.GetElementsByTagName("GetSingleItemResponse")[0]).GetElementsByTagName("Title")[0].InnerText;
                }
                catch
                {
                    title = "No Title Found";
                }

                command.Parameters.Add(new SqlParameter("@ItemTitle", title));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@ItemTitle", "TESTITEMTITLE"));
            }

            command.Parameters.Add(new SqlParameter("@ItemNumber", data.ItemNumber));
            command.Parameters.Add(new SqlParameter("@ConsignmentID", data.ConsignmentID));
            command.Parameters.Add(new SqlParameter("@SKU", data.SKU));
            command.Parameters.Add(new SqlParameter("@IsTest", data.IsTest));
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
        [Route("RemoveItem")]
        public int RemoveItem(int itemID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_removeItem");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ItemID", itemID));
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

        public int ChangeItemTitle(int itemID, string newTitle)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_changeItemTitle");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ItemID", itemID));
            command.Parameters.Add(new SqlParameter("@NewTitle", newTitle));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            if (table.Rows.Count > 0 && table.Rows[0][0].ToString() == newTitle)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        [HttpPost]
        [Route("ChangeItemNumber")]
        public int ChangeItemNumber(ChangeItemNumber cin)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_changeItemNumber");
            command.CommandType = CommandType.StoredProcedure;

            if (cin.IsTest == "0")
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("http://open.api.ebay.com/shopping?callname=GetSingleItem&responseencoding=XML&appid=GregoryM-mailer-PRD-a45ed6035-97c14545&siteid=0&version=967&ItemID=" + cin.NewItemNumber);

                string title;
                try
                {
                    title = ((XmlElement)doc.GetElementsByTagName("GetSingleItemResponse")[0]).GetElementsByTagName("Title")[0].InnerText;
                }
                catch
                {
                    title = "No Title Found";
                }
                
                ChangeItemTitle(int.Parse(cin.ItemID), title);
            }
            else
            {
                ChangeItemTitle(int.Parse(cin.ItemID), "TESTITEMTITLE");
            }

            command.Parameters.Add(new SqlParameter("@ItemID", cin.ItemID));
            command.Parameters.Add(new SqlParameter("@NewItemNumber", cin.NewItemNumber));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            if (table.Rows.Count > 0 && table.Rows[0][0].ToString() == cin.NewItemNumber)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        [HttpPost]
        [Route("ChangeConsignmentID")]
        public int ChangeConsignmentID(ChangeConsignmentID cci)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_changeConsignmentID");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ItemID", cci.ItemID));
            command.Parameters.Add(new SqlParameter("@NewConsignmentID", cci.NewConsignmentID));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            if (table.Rows.Count > 0 && table.Rows[0][0].ToString() == cci.NewConsignmentID.ToString())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        [HttpPost]
        [Route("ChangeSKU")]
        public int ChangeSKU(ChangeSKU cs)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_changeSKU");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ItemID", cs.itemID));
            command.Parameters.Add(new SqlParameter("@NewSKU", cs.newSKU));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            if (table.Rows.Count > 0 && table.Rows[0][0].ToString() == cs.newSKU.ToString())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        [HttpGet]
        [Route("GetItemTitle")]
        public string GetItemTitle(int itemID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getItemTitle");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ItemID", itemID));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            return table.Rows[0][0].ToString();
        }

        [HttpGet]
        [Route("GetItemNumber")]
        public string GetItemNumber(int itemID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getItemNumber");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ItemID", itemID));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            return table.Rows[0][0].ToString();
        }

        [HttpGet]
        [Route("GetItemID")]
        public string GetItemID(string itemNumber)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getItemID");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ItemNumber", itemNumber));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            return table.Rows[0][0].ToString();
        }

        [HttpGet]
        [Route("GetItemConsignmentID")]
        public string GetItemConsignmentID(int itemID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getItemConsignmentID");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ItemID", itemID));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            return table.Rows[0][0].ToString();
        }

        [HttpGet]
        [Route("GetSKU")]
        public string GetSKU(int itemID)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getSKU");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ItemID", itemID));
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
