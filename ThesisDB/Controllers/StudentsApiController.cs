using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThesisDB.Models;

namespace ThesisDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsApiController : ControllerBase
    {
        private readonly ThesisDbContext _context;

        public StudentsApiController(ThesisDbContext context)
        {
            _context = context;
        }

        // GET: api/StudentsApi/search?q=suchstring
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q) || q.Length < 3)
            {
                return Ok(new object[] { });
            }

            var lowerQ = q.ToLower();
            var students = await _context.Students
                .Where(s => s.FirstName.ToLower().Contains(lowerQ) || s.LastName.ToLower().Contains(lowerQ))
                .Take(10)
                .Select(s => new
                {
                    id = s.Id,
                    firstName = s.FirstName,
                    lastName = s.LastName,
                    matriculationNumber = s.MatriculationNumber,
                    displayName = s.FirstName + " " + s.LastName + " (" + s.MatriculationNumber + ")"
                })
                .ToListAsync();

            return Ok(students);
        }

        // POST: api/StudentsApi
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            var result = new
            {
                id = student.Id,
                firstName = student.FirstName,
                lastName = student.LastName,
                matriculationNumber = student.MatriculationNumber,
                displayName = student.FirstName + " " + student.LastName + " (" + student.MatriculationNumber + ")"
            };

            return CreatedAtAction(nameof(Search), new { id = student.Id }, result);
        }
    }
}