using WebApiCors.Models;
using System.Data;

namespace WebApiCors.converter
{
    public class Converter
    {
        public List<Todo> dataTableToList(DataTable dataTable)
        {
            List<Todo> list = new List<Todo>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Todo todo = new Todo();
                todo.id = dataTable.Rows[i]["id"].ToString();
                todo.account_id = dataTable.Rows[i]["account_id"].ToString();
                todo.name = dataTable.Rows[i]["name"].ToString();
                todo.priority = dataTable.Rows[i]["priority"].ToString();
                todo.isCompleted = (bool)dataTable.Rows[i]["completed"];
                list.Add(todo);
            }

            return list;
        }
    }
}
