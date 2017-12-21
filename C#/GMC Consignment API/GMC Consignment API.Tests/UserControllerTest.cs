using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Data;
using GMC_Consignment_API.Controllers;
using GMC_Consignment_API.Models;

namespace GMC_Consignment_API.Tests
{
    [TestClass]
    public class UserControllerTest
    {
        UserController controller = new UserController();

        [TestMethod]
        public void AddUserTest()
        {
            UserCredentials creds = new UserCredentials();
            creds.Username = "TESTUSER";
            creds.Password = "TESTPASSWORD";
            creds.Name = "TESTNAME";
            creds.Email = "TESTEMAIL";
            creds.IsTest = 1;

            int result = controller.AddUser(creds);
            Assert.AreEqual(result, 1);
        }
        
        [TestMethod]
        public void AddConsignmentTest()
        {
            ConsignmentInformation info = new ConsignmentInformation();
            info.SKUMin = "0";
            info.SKUMax = "0";
            info.ConsignmentName = "TESTCONSIGNMENT";
            info.Total = "0";
            info.IsTest = 1;

            int result = controller.AddConsignment(info);
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void AssignConsignmentTest()
        {
            Assignment assignment = new Assignment();
            assignment.UserID = int.Parse(GetUserID());
            assignment.ConsignmentID = int.Parse(GetConsignmentID());

            int result = controller.AssignConsignment(assignment);
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void UnassignConsignmentTest()
        {
            Assignment assignment = new Assignment();
            string pair = GetTestConsignmentPair();
            assignment.UserID = int.Parse(pair.Split(',')[0]);
            assignment.ConsignmentID = int.Parse(pair.Split(',')[1]);

            int result = controller.UnassignConsignment(assignment);
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void RemoveUserTest()
        {
            int result = controller.RemoveUser(int.Parse(GetUserID()));
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void RemoveConsignmentTest()
        {
            int result = controller.RemoveConsignment(int.Parse(GetUserID()));
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void ChangeUsernameTest()
        {
            NewUsername nu = new NewUsername();
            nu.UserID = GetUserID();
            nu.NewUsernameString = "TESTUSERNAME2";

            int result = controller.ChangeUsername(nu);
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void ChangePasswordTest()
        {
            NewPassword np = new NewPassword();
            np.UserID = GetUserID();
            np.NewPasswordString = "TESTPASSWORD2";

            int result = controller.ChangePassword(np);
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void ChangeEmailTest()
        {
            NewEmail ne = new NewEmail();
            ne.UserID = GetUserID();
            ne.NewEmailString = "TESTEMAIL2";

            int result = controller.ChangeEmail(ne);
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void ChangeNameTest()
        {
            NewName nn = new NewName();
            nn.UserID = GetUserID();
            nn.NewNameString = "TESTNAME2";

            int result = controller.ChangeName(nn);
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void ChangeSKURangeTest()
        {
            SKURange range = new SKURange();
            range.ConsignmentID = int.Parse(GetConsignmentID());
            range.Min = "1";
            range.Max = "1";

            int result = controller.ChangeSKURange(range);
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void ChangeConsignmentNameTest()
        {
            NewConsignmentName ncn = new NewConsignmentName();
            ncn.ConsignmentID = GetConsignmentID();
            ncn.NewConsignmentNameString = "TESTCONSIGNMENT2";

            int result = controller.ChangeConsignmentName(ncn);
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void ChangeConsignmentStatusTest()
        {
            NewConsignmentStatus ncs = new NewConsignmentStatus();
            ncs.ConsignmentID = GetConsignmentID();
            ncs.NewConsignmentStatusInt = 1;

            int result = controller.ChangeConsignmentStatus(ncs);
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void ChangeTotalTest()
        {
            NewTotal nt = new NewTotal();
            nt.ConsignmentID = GetConsignmentID();
            nt.NewTotalString = "TESTTOTAL2";

            int result = controller.ChangeTotal(nt);
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void ChangeMoneyMadeTest()
        {
            NewMoneyMade nmm = new NewMoneyMade();
            nmm.ConsignmentID = GetConsignmentID();
            nmm.NewMoneyMadeString = "TESTMONEYMADE2";

            int result = controller.ChangeMoneyMade(nmm);
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void GetUsernameTest()
        {
            string username = controller.GetUsername(int.Parse(GetUserID()));
            Assert.AreNotEqual(username, "");
        }

        [TestMethod]
        public void GetPasswordTest()
        {
            string password = controller.GetPassword(int.Parse(GetUserID()));
            Assert.AreNotEqual(password, "");
        }
        
        [TestMethod]
        public void GetEmailTest()
        {
            string email = controller.GetEmail(int.Parse(GetUserID()));
            Assert.AreNotEqual(email, "");
        }

        [TestMethod]
        public void GetNameTest()
        {
            string name = controller.GetName(int.Parse(GetUserID()));
            Assert.AreNotEqual(name, "");
        }

        [TestMethod]
        public void GetConsignmentTest()
        {
            string consignment = controller.GetConsignment(int.Parse(GetUserID()));
            Assert.AreNotEqual(consignment, null);
        }

        [TestMethod]
        public void GetUserIDTest()
        {
            string userID = controller.GetUserID("TESTUSER");
            Assert.AreNotEqual(userID, "");
        }

        [TestMethod]
        public void AuthenticateTest()
        {
            int result = controller.Authenticate("TESTUSER", "TESTPASSWORD");
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void GetUserTest()
        {
            AssignConsignmentTest();
            string userID = controller.GetUser(int.Parse(GetTestConsignmentPair().Split(',')[1]));
            Assert.AreNotEqual(userID, "");
        }

        [TestMethod]
        public void GetSKURangeTest()
        {
            string skuRange = controller.GetSKURange(int.Parse(GetConsignmentID()));
            Assert.AreNotEqual(skuRange, "");
        }

        [TestMethod]
        public void GetConsignmentIDTest()
        {
            string consignmentID = controller.GetConsignmentID("TESTCONSIGNMENT");
            Assert.AreNotEqual(consignmentID, "");
        }

        [TestMethod]
        public void GetConsignmentNameTest()
        {
            string consignmentName = controller.GetConsignmentName(int.Parse(GetConsignmentID()));
            Assert.AreNotEqual(consignmentName, "");
        }

        [TestMethod]
        public void GetConsignmentStatusTest()
        {
            int consignmentStatus = controller.GetConsignmentStatus(int.Parse(GetConsignmentID()));
            Assert.AreNotEqual(consignmentStatus, null);
        }

        [TestMethod]
        public void GetTotalTest()
        {
            string total = controller.GetTotal(int.Parse(GetConsignmentID()));
            Assert.AreNotEqual(total, "");
        }

        [TestMethod]
        public void GetMoneyMadeTest()
        {
            string moneyMade = controller.GetNetSales(int.Parse(GetConsignmentID()));
            Assert.AreNotEqual(moneyMade, "");
        }

        public string GetUserID()
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getTestUserID");
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

        public string GetTestConsignmentPair()
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            SqlCommand command = new SqlCommand("usp_getTestConsignmentPair");
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = connection;

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            return table.Rows[0][0].ToString() + "," + table.Rows[0][1].ToString();
        }
    }
}
