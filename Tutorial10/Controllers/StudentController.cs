using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutorial10.Entities;
using Tutorial10.Services;

namespace Tutorial10.Controllers
{
    [ApiController]
    [Route("api/student")]
    public class StudentController : ControllerBase
    {
        IStudentDbService _db;

        public StudentController(IStudentDbService db)
        {
            _db = db;
        }


        [HttpGet]
        public IActionResult GetStudent()
        {
            var students = _db.GetStudents();
            if (students.Count > 0) return Ok(students);

            return NotFound("Student table is empty.");
        }

        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            var response = _db.AddStudent(student);
            if (response) return Ok("Student has added.");

            return BadRequest("Index number already exists.");
        }

        [HttpPut]
        public IActionResult UpdateStudent(Student student)
        {
            var response = _db.UpdateStudent(student);

            if (response) return Ok("Student has updated.");

            return BadRequest("Identifier does not exists.");
        }

        [HttpDelete("{index}")]
        public IActionResult DeleteStudent(string index)
       {
            var response = _db.DeleteStudent(index);
    
            if (response) {
                return Ok("Student has been deleted.");
            }
            return NotFound("Stundent does not found.");
        }
    }
}
