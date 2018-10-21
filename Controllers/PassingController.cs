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
    public class PassingController : ControllerBase {

        //SQL Connetion String
        //Database.Database myDatabase = new Database.Database();
        string sqlcommstring = Database.Database.SqlConnationString;

        // GET api/values
        [HttpPost]
        public ActionResult Post (Models.passing myPassing) {
            using (var cn = new SqlConnection (sqlcommstring)) {
                cn.Open ();
                string sql = @"DECLARE @HIS_SN VARCHAR(100), @TRANSACTION_SN VARCHAR(100), @AMOUNT FLOAT, @BALANCE FLOAT;
                            SELECT @HIS_SN='PSHS'+CONVERT(VARCHAR(8), GETDATE(), 112)+CONVERT(VARCHAR(10), COUNT(HIS_SN)+1)
                            FROM MLFF_DB..Passing_His;
                            SELECT @TRANSACTION_SN='TSN'+CONVERT(VARCHAR(8), GETDATE(), 112)+CONVERT(VARCHAR(10), COUNT(TRANSACTION_SN)+1)
                            FROM MLFF_DB..Virtual_Account;
                            SELECT @AMOUNT=GANTRY_FEE
                            FROM MLFF_DB..Gantrys
                            WHERE GANTRY_SN=@GANTRY_SN;
                            INSERT INTO Passing_His
                                (HIS_SN, USER_SN, GANTRY_SN, PASSING_TIME)
                            VALUES(@HIS_SN, @USER_SN, @GANTRY_SN, GETDATE());
                            SELECT TOP 1 @BALANCE=BALANCE FROM MLFF_DB..Virtual_Account WHERE USER_SN=@USER_SN ORDER BY TRANSACTION_TIME DESC;
                            SET @BALANCE=@BALANCE-@AMOUNT;
                            INSERT INTO MLFF_DB..Virtual_Account
                                (TRANSACTION_SN, USER_SN, TRANSACTION_TYPE, AMOUNT, BALANCE, TRANSACTION_TIME, DESCRIPTION_NOTE)
                            VALUES(@TRANSACTION_SN, @USER_SN, 'DEBIT', @AMOUNT, @BALANCE, GETDATE(), @HIS_SN);";
                cn.Query<float> (sql, new { @USER_SN = myPassing.USER_SN, @GANTRY_SN = myPassing.GANTRY_SN });
            }

            return Ok ();
        }
    }
}