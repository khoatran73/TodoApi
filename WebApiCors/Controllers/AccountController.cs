using Microsoft.AspNetCore.Mvc;
using WebApiCors.result;
using Microsoft.Data.SqlClient;
using WebApiCors.Models;
using System.Diagnostics;
using System.Web.Http.Results;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiCors.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        string stringConnection = @"Data Source=KHOA-PRO\SQLEXPRESS
            ; Initial Catalog=TodoApp
            ; Integrated Security=True
            ; TrustServerCertificate=True
            ; MultipleActiveResultSets=True";

        // POST: api/Account
        [HttpPost]
        public IActionResult Post([FromBody] Account account)
        {
            Result result = new Result();
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(stringConnection);
            sqlConnection.Open();

            string query1 = @"select * from Account where id = '" + account.id + "'";

            SqlCommand cmd;
            cmd = new SqlCommand(query1, sqlConnection);
            SqlDataReader sqlDataReader;
            sqlDataReader = cmd.ExecuteReader();

            Debug.WriteLine(sqlDataReader);

            dataTable.Load(sqlDataReader);

            if (dataTable.Rows.Count > 0)
            {
                // have account
                result.code = 1;
                result.message = "account already exist";
                sqlConnection.Close();
                return new JsonResult(result);
            }
            string query2 = @"insert into Account values
                    (
                    '" + account.id + @"'
                    ,'" + account.email + @"'
                    ,'" + account.name + @"'
                    ,'" + account.url + @"'
                    )
                    ";

            cmd = new SqlCommand(query2, sqlConnection);
            cmd.ExecuteReader();

            result.code = 0;
            result.message = "success";
            result.account = account;

            sqlConnection.Close();
            return new JsonResult(result);

        }
    }
}
