using System.ComponentModel.DataAnnotations;

namespace JobInterviewAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        public string? DisplayName { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
