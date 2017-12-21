using GMC_Consignment_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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

        [HttpPost]
        [Route("ChangeItemTitle")]
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

        [HttpGet]
        [Route("GetItemNumberBySKU")]
        public string GetItemNumberBySKU(string SKU)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getItemNumberBySKU");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@SKU", SKU));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            return table.Rows[0][0].ToString();
        }

        [HttpGet]
        [Route("GetItemIDBySKU")]
        public string GetItemIDBySKU(string SKU)
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getItemIDBySKU");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@SKU", SKU));
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            return table.Rows[0][0].ToString();
        }

        [HttpGet]
        [Route("GetItemDataBySKU")]
        public ItemInformation GetItemDataBySKU(string SKU)
        {
            ItemInformation info = new ItemInformation();
            info.ItemID = GetItemIDBySKU(SKU);
            info.ItemTitle = GetItemTitle(int.Parse(info.ItemID));
            info.ItemNumber = GetItemNumberBySKU(SKU);
            info.SKU = SKU;
            info.ConsignmentID = GetItemConsignmentID(int.Parse(info.ItemID));

            return info;
        }

        [HttpGet]
        [Route("GetItemNumbersBySKURange")]
        public string GetItemNumbersBySKURanges(string mins, string maxs, string prefixes)
        {
            string[] minArray = mins.Split(',');
            string[] maxArray = maxs.Split(',');
            string[] prefixArray = prefixes.Split(',');

            string full = "";
            for (int i = 0; i < minArray.Length; i++)
            {
                string itemNumbers = "";
                for (int e = int.Parse(minArray[i]); e <= int.Parse(maxArray[i]); e++)
                {
                    string itemNumber = "";
                    try
                    {
                        itemNumber = GetItemNumberBySKU((prefixArray[i] + e).ToString());
                    }
                    catch
                    {
                        itemNumber = "N";
                    }

                    string status = "";
                    try
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load("http://open.api.ebay.com/shopping?callname=GetSingleItem&responseencoding=XML&appid=GregoryM-mailer-PRD-a45ed6035-97c14545&siteid=0&version=967&ItemID=" + itemNumber);
                        string result = ((XmlElement)((XmlElement)doc.GetElementsByTagName("GetSingleItemResponse")[0]).GetElementsByTagName("Item")[0]).GetElementsByTagName("ListingStatus")[0].InnerText;

                        if (result == "Completed")
                        {
                            status = "c";
                        }
                        else if (result == "Ended")
                        {
                            status = "u";
                        }
                        else if (result == "Active")
                        {
                            status = "a";
                        }
                    }
                    catch
                    {
                        status = "n";
                    }

                    itemNumbers += "," + itemNumber + "~" + status;
                }

                itemNumbers = itemNumbers.Substring(1) + ",";
                full += itemNumbers;
            }

            return full;
        }

        [HttpPost]
        [Route("GetItemTitles")]
        public string GetItemTitles(ItemNumberList numbers)
        {
            string[] nums = numbers.ItemNumbers.Split(',');
            string titles = "";
            foreach (string number in nums)
            {
                try
                {
                    string title = GetItemTitle(int.Parse(GetItemID(number)));
                    StringBuilder builder = new StringBuilder(title);
                    builder.Replace(',', ' ');
                    title = builder.ToString();

                    if (title.Length >= 45)
                    {
                        titles += "," + title.Substring(0, 42) + "...";
                    }
                    else
                    {
                        titles += "," + title;
                    }
                }
                catch
                {
                    titles += ",N";
                }
            }
            titles = titles.Substring(1);

            return titles;
        }

        [HttpPost]
        [Route("GetItemEndDates")]
        public string GetItemEndDates(ItemNumberList list)
        {
            string[] nums = list.ItemNumbers.Split(',');
            string endDates = "";

            foreach (string number in nums)
            {
                XmlDocument doc = new XmlDocument();

                string number2 = number.Substring(0, number.Length - 2);
                try
                {
                    doc.Load("http://open.api.ebay.com/shopping?callname=GetSingleItem&responseencoding=XML&appid=GregoryM-mailer-PRD-a45ed6035-97c14545&siteid=0&version=967&ItemID=" + number2);
                    string endDate = ((XmlElement)((XmlElement)doc.GetElementsByTagName("GetSingleItemResponse")[0]).GetElementsByTagName("Item")[0]).GetElementsByTagName("EndTime")[0].InnerText;
                    string[] components1 = endDate.Split('T');
                    string[] date = components1[0].Split('-');
                    string[] time = components1[1].Split(':');
                    time[2] = time[2].Substring(0, time[2].IndexOf('.'));
                    DateTime endTimeDt = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]), int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2]));
                    endTimeDt = endTimeDt.AddHours(-8);
                    endDate = endTimeDt.Month + "/" + endTimeDt.Day + "/" + endTimeDt.Year + " " + endTimeDt.Hour + ":" + endTimeDt.Minute + ":" + endTimeDt.Second + " PST";
                    endDates += "~" + endDate;
                }
                catch
                {
                    endDates += "~No End Time Found";
                }
            }

            endDates = endDates.Substring(1);
            return endDates;
        }

        [HttpPost]
        [Route("GetItemPrices")]
        public string GetItemPrices(ItemNumberList items)
        {
            string[] nums = items.ItemNumbers.Split(',');
            string prices = "";

            foreach (string number in nums)
            {
                string number2 = number.Substring(0, number.Length - 2);
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load("http://open.api.ebay.com/shopping?callname=GetSingleItem&responseencoding=XML&appid=GregoryM-mailer-PRD-a45ed6035-97c14545&siteid=0&version=967&ItemID=" + number2);
                    string price = ((XmlElement)((XmlElement)doc.GetElementsByTagName("GetSingleItemResponse")[0]).GetElementsByTagName("Item")[0]).GetElementsByTagName("ConvertedCurrentPrice")[0].InnerText;
                    prices += ",$" + price;
                }
                catch
                {
                    prices += ",No Price Found";
                }
            }

            prices = prices.Substring(1);

            return prices;
        }
    }
}
