namespace WebApiCors.Models
{
    public class Todo
    {
        public string id { get; set; }
        public string account_id { get; set; }
        public string name { get; set; }
        public string priority { get; set; }
        public bool isCompleted { get; set; }
    }
}
