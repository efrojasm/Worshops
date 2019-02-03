using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiWorkshop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiWorkshop.Controllers
{
    [Produces("application/json")]
    [Route("api/Students")]
    public class StudentController : Controller
    {
        private readonly StudentDBContext context;

        public StudentController(StudentDBContext context)
        {
            this.context = context;
        }
        // GET: api/Students
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return context.Students.ToList();
        }

        [HttpGet("{id}", Name ="Student Created")]
        public IActionResult GetById(int id)
        {
            var student = context.Students.FirstOrDefault(x => x.Id == id);

            if(student==null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            if(ModelState.IsValid)
            {
                context.Students.Add(student);
                context.SaveChanges();
                return new CreatedAtRouteResult("Student Created", new { id = student.Id }, student);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Student student, int id)
        {
            if (student.Id!=id)
            {
                return BadRequest();
            }

            context.Entry(student).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = context.Students.FirstOrDefault(x => x.Id == id);

            if (student==null)
            {
                return NotFound();
            }

            context.Students.Remove(student);
            context.SaveChanges();
            return Ok(student);
        }


    }
}
