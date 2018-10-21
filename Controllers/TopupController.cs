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
    public class TopupController : ControllerBase {

        //SQL Connetion String
        //Database.Database myDatabase = new Database.Database();
        string sqlcommstring = Database.Database.SqlConnationString;

        // GET api/values
        [HttpPost]
        public ActionResult Post (Models.topup myTopup) {
            using (var cn = new SqlConnection (sqlcommstring)) {
                cn.Open ();
                string sql = @"DECLARE @TRANSACTION_SN VARCHAR(100), @BALANCE FLOAT;
                            SELECT @TRANSACTION_SN='TSN'+CONVERT(VARCHAR(8), GETDATE(), 112)+CONVERT(VARCHAR(10), COUNT(TRANSACTION_SN)+1)
                            FROM MLFF_DB..Virtual_Account;
                            SELECT TOP 1 @BALANCE=BALANCE FROM MLFF_DB..Virtual_Account WHERE USER_SN=@USER_SN ORDER BY TRANSACTION_TIME DESC;
                            SET @BALANCE=ISNULL(@BALANCE, 0)+@AMOUNT;
                            INSERT INTO MLFF_DB..Virtual_Account
                                (TRANSACTION_SN, USER_SN, TRANSACTION_TYPE, AMOUNT, BALANCE, TRANSACTION_TIME, DESCRIPTION_NOTE)
                            VALUES(@TRANSACTION_SN,
                                    @USER_SN,
                                    'CREDIT',
                                    @AMOUNT,
                                    @BALANCE,
                                    MLFF_DB.dbo.GetLocalDate(DEFAULT),
                                    NULL
                            );";
                cn.Query<float> (sql, new { @USER_SN = myTopup.USER_SN, @AMOUNT = myTopup.AMOUNT });
            }

            return Ok ();
        }
    }
}