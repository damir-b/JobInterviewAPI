using JobInterviewAPI.Models;

namespace JobInterviewAPI.Data
{
    public class DataSeeder
    {
        private readonly AppDbContext _db;
        public DataSeeder(AppDbContext db) => _db = db;


        public Task SeedAsync()
        {
            if (!_db.Users.Any())
            {
                var u1 = new User { Username = "testuser1", DisplayName = "Testni user 1" };
                var u2 = new User { Username = "testuser2", DisplayName = "Testni user 2" };
                _db.Users.AddRange(u1, u2);


                _db.Comments.AddRange(
                new Comment { User = u1, Text = "Hello from Testni user 1" },
                new Comment { User = u2, Text = "Testni user 2 - first comment" }
                );


                _db.SaveChanges();
            }
            return Task.CompletedTask;
        }
    }
}
