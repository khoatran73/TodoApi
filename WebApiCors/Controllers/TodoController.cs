using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Web.Http.Results;
using WebApiCors.Models;
using WebApiCors.result;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApiCors.converter;
using System.Diagnostics;

namespace WebApiCors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : Controller
    {
        string stringConnection = @"Data Source=KHOA-PRO\SQLEXPRESS
            ; Initial Catalog=TodoApp
            ; Integrated Security=True
            ; TrustServerCertificate=True
            ; MultipleActiveResultSets=True";

        //get all todos of user
        // GET: api/Todo
        [Route("/api/todo/{account_id}")]
        [HttpGet]
        public IActionResult GetAllAccountTodo(string account_id)
        {
            string query = @"select * from dbo.Todo 
                    where account_id = '" + account_id + "'" +
                    " order by created asc";
            Result result;
            DataTable dataTable = new DataTable();

            SqlConnection sqlConnection = new SqlConnection(stringConnection);
            sqlConnection.Open();

            SqlCommand cmd;
            cmd = new SqlCommand(query, sqlConnection);

            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            dataTable.Load(sqlDataReader);

            // Convert data table to list
            Converter converter = new Converter();
            List<Todo> todoList = converter.dataTableToList(dataTable);

            result = new Result(0, "success");
            result.todoList = todoList;

            sqlConnection.Close();

            return new JsonResult(result);
        }

        // POST: api/TodoB
        [HttpPost]
        [Route("/api/todo")]
        public IActionResult Post([FromBody] Todo todo)
        {
            Result result = new Result();
            SqlConnection sqlConnection = new SqlConnection(stringConnection);
            sqlConnection.Open();

            string query = @"insert into Todo values
                    (
                    '" + todo.id + @"'
                    ,'" + todo.account_id + @"'
                    ,N'" + todo.name + @"'
                    ,'" + todo.priority + @"'
                    ,'" + todo.isCompleted + @"'
                    ,'" + DateTime.Now + @"'
                    )
                    ";

            SqlCommand cmd;
            cmd = new SqlCommand(query, sqlConnection);
            cmd.ExecuteReader();

            result.code = 0;
            result.message = "success";

            sqlConnection.Close();

            return new JsonResult(result);
        }

        // PUT: api/Todo/id
        [Route("/api/todo/{id}")]
        [HttpPut]
        public IActionResult Put(string id, [FromBody] Todo todo)
        {
            Result result = new Result();
            SqlConnection sqlConnection = new SqlConnection(stringConnection);
            sqlConnection.Open();

            string query = @"update Todo 
                    set name = N'" + todo.name + @"'
                    , priority = '" + todo.priority + @"'
                    , completed = '" + todo.isCompleted + @"'
                    where id = '" + id + "' and account_id = '" + todo.account_id + "'";

            SqlCommand cmd;
            cmd = new SqlCommand(query, sqlConnection);

            cmd.ExecuteReader();

            result.code = 0;
            result.message = "success";

            sqlConnection.Close();

            return new JsonResult(result);
        }

        // DELETE: api/Todo/id/account_id
        [Route("/api/todo/{id}/{account_id}")]
        [HttpDelete]
        public IActionResult Delete(string id, string account_id)
        {
            Result result = new Result();
            SqlConnection sqlConnection = new SqlConnection(stringConnection);
            sqlConnection.Open();

            string query = @"delete from Todo where id = '" + id + "' and account_id = '" + account_id + "'";

            SqlCommand cmd;
            cmd = new SqlCommand(query, sqlConnection);
            cmd.ExecuteReader();

            result.code = 0;
            result.message = "success";

            sqlConnection.Close();

            return new JsonResult(result);
        }
    }
}
