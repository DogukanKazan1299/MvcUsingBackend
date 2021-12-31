using CRUDAPI.Data;
using CRUDAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationProject.Models;

namespace CRUDAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        Context _context;
        public StudentsController(Context context)
        {
            _context = context;
        }
        [HttpGet]
        public List<Student> Get()
        {
            return _context.Students.ToList();
        }
        [HttpGet("{Id}")]
        public Student GetStudent(int Id)
        {
            var student = _context.Students.Where(a => a.Id == Id).SingleOrDefault();
            return student;
        }
        [HttpPost]
        public IActionResult PostStudent([FromBody]Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            _context.Students.Add(student);
            _context.SaveChanges();
            return Ok();
            
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
