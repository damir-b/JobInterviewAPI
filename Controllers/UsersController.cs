using JobInterviewAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobInterviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;
        public UsersController(AppDbContext db) => _db = db;


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _db.Users.Include(u => u.Comments).ToListAsync();
            return Ok(users);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _db.Users.Include(u => u.Comments).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}
