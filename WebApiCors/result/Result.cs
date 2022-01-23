using Newtonsoft.Json;
using WebApiCors.Models;

namespace WebApiCors.result
{
    public class Result
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<Todo> todoList { get; set; }
        public Account account { get; set; }

        [JsonConstructor]
        public Result(int code, string message)
        {
            this.code = code;
            this.message = message;
        }

        [JsonConstructor]
        public Result()
        {
            this.code = 0;
            this.message = "";
        }
    }
}
