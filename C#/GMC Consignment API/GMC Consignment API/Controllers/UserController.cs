using GMC_Consignment_API.Models;
using System;
using System.Collections.Generic;
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
        [Route("RegisterUser")]
        public int RegisterUser(UserCredentials creds)
        {
            SqlConnection connection = new SqlConnection();
        }
    }
}
