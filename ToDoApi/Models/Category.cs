namespace ToDoApi.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<TaskCard> Tasks { get; set; }

    }
}
