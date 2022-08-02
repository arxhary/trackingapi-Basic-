using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trackingapi.Data;
using trackingapi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace trackingapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IssueDbContext _context;

        public IssueController(IssueDbContext context)
        {
            _context = context;
        }

        //List
        // GET: api/<IssueController>
        [HttpGet]
        public async Task<IEnumerable<Issue>> Get()
        {
            return await _context.Issues.ToListAsync();
        }

        //Filter
        // GET api/<IssueController>/5
        [HttpGet("id")]
        [ProducesResponseType(typeof(Issue), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            return issue == null ? NotFound() : Ok(issue);
        }

        // Create
        // POST api/<IssueController>
        [HttpPost]
        [ProducesResponseType(typeof(Issue), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(Issue issue)
        {
            await _context.Issues.AddAsync(issue);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById),new {id= issue.Id},issue);
        }

        // Update
        // PUT api/<IssueController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, Issue issue)
        {
            if (id != issue.Id)
            {
                return BadRequest();
            }

            _context.Entry(issue).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Delete
        // DELETE api/<IssueController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var issueToDelete = await _context.Issues.FindAsync(id);
            if(issueToDelete == null) return NotFound();

            _context.Issues.Remove(issueToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
