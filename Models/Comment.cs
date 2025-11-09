using System.ComponentModel.DataAnnotations;

namespace JobInterviewAPI.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
