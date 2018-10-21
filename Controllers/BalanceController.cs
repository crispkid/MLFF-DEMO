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
    public class BalanceController : ControllerBase {

        //SQL Connetion String
        //Database.Database myDatabase = new Database.Database();
        string sqlcommstring = Database.Database.SqlConnationString;

        // GET api/values
        [HttpGet ("{USER_SN}")]
        public ActionResult<IEnumerable<float>> Get (string USER_SN) {
            IEnumerable<float> myBalance = null;
            using (var cn = new SqlConnection (sqlcommstring)) {
                cn.Open ();
                string sql = @"SELECT TOP 1
                                    BALANCE
                                FROM MLFF_DB..Virtual_Account WITH (NOLOCK)
                                WHERE USER_SN=@USER_SN
                                ORDER BY TRANSACTION_Time DESC;";
                myBalance = cn.Query<float> (sql, new { @USER_SN = USER_SN });
            }

            return Ok (myBalance);
        }
    }
}