using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GMC_Consignment_API.Controllers;
using GMC_Consignment_API.Models;
using System.Data.SqlClient;
using System.Data;

namespace GMC_Consignment_API.Tests
{
    [TestClass]
    public class ItemControllerTest
    {
        ItemController controller = new ItemController();

        [TestMethod]
        public void AddItemTest()
        {
            ItemData data = new ItemData();
            data.ItemNumber = "TESTITEMNUMBER";
            data.ConsignmentID = GetConsignmentID();
            data.SKU = "0";
            data.IsTest = "1";

            int result = controller.AddItem(data);
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void RemoveItemTest()
        {
            int result = controller.RemoveItem(int.Parse(GetItemID()));
            Assert.AreEqual(result, 1);
        } 

        [TestMethod]
        public void ChangeItemTitleTest()
        {
            int result = controller.ChangeItemTitle(int.Parse(GetItemID()), "TESTITEMTITLE2");
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void ChangeItemNumberTest()
        {
            ChangeItemNumber cin = new ChangeItemNumber();
            cin.ItemID = GetItemID();
            cin.NewItemNumber = "TESTNUMBER2";
            cin.IsTest = "1";

            int result = controller.ChangeItemNumber(cin);
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void ChangeConsignmentIDTest()
        {
            ChangeConsignmentID cci = new ChangeConsignmentID();
            cci.ItemID = GetItemID();
            cci.NewConsignmentID = GetConsignmentID();

            int result = controller.ChangeConsignmentID(cci);
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void ChangeSKUTest()
        {
            ChangeSKU cs = new ChangeSKU();
            cs.itemID = GetItemID();
            cs.newSKU = "1";

            int result = controller.ChangeSKU(cs);
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void GetItemTitleTest()
        {
            string result = controller.GetItemTitle(int.Parse(GetItemID()));
            Assert.AreNotEqual(result, "");
        }

        [TestMethod]
        public void GetItemNumberTest()
        {
            string result = controller.GetItemNumber(int.Parse(GetItemID()));
            Assert.AreNotEqual(result, "");
        }

        [TestMethod]
        public void GetItemIDTest()
        {
            string result = controller.GetItemID("TESTITEMNUMBER");
            Assert.AreNotEqual(result, "");
        }

        [TestMethod]
        public void GetConsignmentItemIDTest()
        {
            string result = controller.GetItemConsignmentID(int.Parse(GetItemID()));
            Assert.AreNotEqual(result, "");
        }

        [TestMethod]
        public void GetSKUTest()
        {
            string result = controller.GetSKU(int.Parse(GetItemID()));
            Assert.AreNotEqual(result, "");
        }

        public string GetItemID()
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getTestItemID");
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            return table.Rows[0][0].ToString();
        }

        public string GetConsignmentID()
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getTestConsignmentID");
            command.CommandType = CommandType.StoredProcedure;
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
