using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Database;
using Microsoft.AspNetCore.Mvc;

namespace MLFF_DEMO.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {

        //SQL Connetion String
        //Database.Database myDatabase = new Database.Database();
        string sqlcommstring = Database.Database.SqlConnationString;

        // GET api/values
        [HttpGet ("{USER_NAME}")]
        public ActionResult<IEnumerable<Models.user_info>> Get (string USER_NAME) {
            IEnumerable<Models.user_info> myUser = null;
            using (var cn = new SqlConnection (sqlcommstring)) {
                cn.Open ();
                string sql = @"SELECT * FROM MLFF_DB..Users WHERE USER_NAME=@USER_NAME;";
                myUser = cn.Query<Models.user_info> (sql, new { @USER_NAME = USER_NAME });
            }

            return Ok (myUser);
        }
    }
}