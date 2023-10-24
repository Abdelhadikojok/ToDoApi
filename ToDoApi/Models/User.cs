using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDoApi.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Image { get; set; }



        [JsonIgnore]
        public List<TaskCard>? Tasks { get; set; }
    }
}
