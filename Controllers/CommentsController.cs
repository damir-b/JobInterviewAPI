using JobInterviewAPI.Attributes;
using JobInterviewAPI.Data;
using JobInterviewAPI.DTOs;
using JobInterviewAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobInterviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public CommentsController(AppDbContext db) => _db = db;


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _db.Comments.ToListAsync();
            return Ok(comments);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var comment = await _db.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null) return NotFound();
            return Ok(comment);
        }

        [HttpPost("{userid}")]
        [SimpleAuthorize]
        public async Task<IActionResult> AddComment(int userid, [FromBody] CreateCommentDto dto)
        {
            var user = await _db.Users.FindAsync(userid);
            if (user == null) return NotFound();

            var comment = new Comment { UserId = userid, Text = dto.Text };
            _db.Comments.Add(comment);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = userid }, comment);
        }

        [HttpDelete("{userid}/{commentId}")]
        public async Task<IActionResult> DeleteComment(int userid, int commentId)
        {
            var comment = await _db.Comments.FirstOrDefaultAsync(c => c.Id == commentId && c.UserId == userid);
            if (comment == null) return NotFound();

            _db.Comments.Remove(comment);
            await _db.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{userid}")]
        public async Task<IActionResult> DeleteAllComments(int userid)
        {
            var comments = _db.Comments.Where(c => c.UserId == userid);
            _db.Comments.RemoveRange(comments);
            await _db.SaveChangesAsync();
            return Ok();
        }

    }
}
