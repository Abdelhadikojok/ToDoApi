using System.ComponentModel.DataAnnotations;

namespace ToDoApi.Dto
{
    public class TaskDto
    {
        public int TaskId { get; set; }


        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string Status { get; set; }

        public string? importance { get; set; }


        public DateTime? DueDate { get; set; }

        public int? EstimateDatenumber { get; set; }

        public string? EstimateDateUnit { get; set; }


        [Required]
        public string Title { get; set; }

        public string? CategoryName { get; set; }
    }
}
