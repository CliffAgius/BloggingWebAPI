namespace BloggingWebAPI.Models
{
    public class ToDo
    {
        public string ID { get; set; }
        public string TodoName { get; set; }
        public string TodoText { get; set; }
        public DateTime DueDate { get; set; }
    }
}
