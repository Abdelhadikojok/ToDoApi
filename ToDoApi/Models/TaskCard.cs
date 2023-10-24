namespace ToDoApi.Models
{
    public class TaskCard
    {
        public int TaskId { get; set; }
        public string Status { get; set; }

        public string? importance { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? EstimateDate { get; set; }
        public string Title { get; set; }

        public int UserId { get; set; } // male search here no need to put the useID it will take it from the object of the user defined down
        public User User { get; set; } 
        public int CategoryId { get; set; } // male search here no need to put the categoryID it will take it from the object of the category defined down
        public Category Category { get; set; }
    }

    // takad hon lama bdi a3mel frogn key w a5od el object is can required w 2za mesh drori tkon mwjodi flazem n3mla nullble bha el tera
    //        public Category? Category { get; set; }
    //        public int CategoryId { get; set; }

}
