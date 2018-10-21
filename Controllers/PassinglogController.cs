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
    public class PassinglogController : ControllerBase {

        //SQL Connetion String
        //Database.Database myDatabase = new Database.Database();
        string sqlcommstring = Database.Database.SqlConnationString;

        // GET api/values
        [HttpGet("{USER_SN}")]
        public ActionResult<IEnumerable<float>> Get (string USER_SN) {
            IEnumerable<Models.passinglog> myPassinglog = null;
            using (var cn = new SqlConnection (sqlcommstring)) {
                cn.Open ();
                string sql = @"SELECT C.GANTRY_NAME, A.PASSING_TIME, B.AMOUNT
                            FROM MLFF_DB..Passing_His A INNER JOIN MLFF_DB..Virtual_Account B WITH (NOLOCK)
                                ON A.HIS_SN= B.DESCRIPTION_NOTE
                                INNER JOIN MLFF_DB..Gantrys C WITH (NOLOCK)
                                on A.GANTRY_SN=C.GANTRY_SN
                            WHERE A.USER_SN=@USER_SN
                            ORDER BY A.PASSING_TIME DESC;";
                myPassinglog = cn.Query<Models.passinglog> (sql, new { @USER_SN = USER_SN });
            }

            return Ok (myPassinglog);
        }
    }
}