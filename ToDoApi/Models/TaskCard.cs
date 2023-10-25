namespace ToDoApi.Models
{
    public class TaskCard
    {
        public int TaskId { get; set; }
        public string Status { get; set; }

        public string? importance { get; set; }
        public DateTime? DueDate { get; set; }
        public int? EstimateDatenumber { get; set; }

        public string? EstimateDateUnit { get; set; }


        public string Title { get; set; }

        public int UserId { get; set; } 
        public User User { get; set; } 
        public int CategoryId { get; set; } 
        public Category Category { get; set; }
    }

   
}
