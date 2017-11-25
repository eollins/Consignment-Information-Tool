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
        [TestMethod]
        public void RegisterUserTest()
        {
            UserController controller = new UserController();
            UserCredentials credentials = new UserCredentials();
            credentials.Username = "TESTUSER";
            credentials.Password = "TESTPASSWORD";
            credentials.SKUMin = "0";
            credentials.SKUMax = "0";
            credentials.Name = "TESTNAME";
            credentials.Email = "TESTEMAIL";
            int result = controller.RegisterUser(credentials);

            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void RemoveUserTest()
        {
            UserController controller = new UserController();
            int result = controller.RemoveUser(GetTestID());

            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void ChangeUsernameTest()
        {
            UserController controller = new UserController();
            int result = controller.ChangeUsername("TESTUSER2", GetTestID());

            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void ChangePasswordTest()
        {
            UserController controller = new UserController();
            int result = controller.ChangePassword("TESTPASSWORD2", GetTestID());

            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void ChangeSKURangeTest()
        {
            SqlConnection connection = new SqlConnection(Connection.connectionString());
            int ID = GetTestID();

            SqlCommand getSKURangeCommand = new SqlCommand("usp_getSKURangeByID");
            getSKURangeCommand.CommandType = CommandType.StoredProcedure;
            getSKURangeCommand.Parameters.Add(new SqlParameter("@UserID", ID));
            getSKURangeCommand.Connection = connection;
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(getSKURangeCommand);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            int SKUMin = int.Parse(table.Rows[0][0].ToString());
            int SKUMax = int.Parse(table.Rows[0][1].ToString());

            UserController controller = new UserController();
            int result = controller.ChangeSKURange(SKUMin + 1, SKUMax + 1, GetTestID());

            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void ChangeNameTest()
        {
            UserController controller = new UserController();
            int result = controller.ChangeName("TESTNAME2", GetTestID());

            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void ChangeEmailTest()
        {
            UserController controller = new UserController();
            int result = controller.ChangeEmail("TESTEMAIL2", GetTestID());

            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void GetUsernameByIDTest()
        {
            UserController controller = new UserController();
            string username = controller.GetUsernameByID(GetTestID());

            Assert.AreNotEqual(username, "");
        }

        [TestMethod]
        public void GetIDByUsernameTest()
        {
            UserController controller = new UserController();
            string userID = controller.GetIDByUsername("TESTUSER");

            Assert.AreNotEqual(userID, "");
        }

        [TestMethod]
        public void GetPasswordByIDTest()
        {
            UserController controller = new UserController();
            string password = controller.GetPasswordByID(GetTestID());

            Assert.AreNotEqual(password, "");
        }

        [TestMethod]
        public void GetSKURangeByIDTest()
        {
            UserController controller = new UserController();
            string SKURange = controller.GetSKURangeByID(GetTestID());

            Assert.AreNotEqual(SKURange, "");
        }

        [TestMethod]
        public void GetNameByIDTest()
        {
            UserController controller = new UserController();
            string name = controller.GetNameByID(GetTestID());

            Assert.AreNotEqual(name, "");
        }

        [TestMethod]
        public void GetEmailByIDTest()
        {
            UserController controller = new UserController();
            string email = controller.GetEmailByID(GetTestID());

            Assert.AreNotEqual(email, "");
        }

        [TestMethod]
        public void AuthenticateTest()
        {
            UserController controller = new UserController();
            LoginInfo info = new LoginInfo();
            info.Username = "TESTUSER";
            info.Password = "TESTPASSWORD";

            int result = controller.Authenticate(info);
            Assert.AreEqual(result, 1);
        }

        public int GetTestID()
        {
            RegisterUserTest();
            return int.Parse(new UserController().GetIDByUsername("TESTUSER"));
        }
    }
}
