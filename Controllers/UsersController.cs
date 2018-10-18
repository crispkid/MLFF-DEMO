using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace MLFF_DEMO.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {

        //SQL Connetion String
        string sqlcommstring = @"Server=tcp:mlff.database.windows.net,1433;Initial Catalog=MLFF_DB;Persist Security Info=False;User ID=mlff_sa;Password=1qaz@WSX;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Models.user_info>> Get () {
            IEnumerable<Models.user_info> myUser = null;
            using (var cn = new SqlConnection (sqlcommstring)) {
                cn.Open ();
                string sql = @"select * from MLFF_DB..Users;";
                myUser = cn.Query<Models.user_info> (sql);
            }
 
            return Ok(myUser);
        }
    }
}