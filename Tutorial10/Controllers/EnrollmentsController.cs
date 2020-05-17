using Microsoft.AspNetCore.Mvc;
using s19551Assingment4.DTOs;
using s19551Assingment4.DTOs.Responce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutorial10.Services;

namespace Tutorial10.Controllers
{
    [ApiController]
    [Route("api/enrollment")]
    public class EnrollmentsController:ControllerBase
    {
        private readonly IStudentDbService _db;

        public EnrollmentsController(IStudentDbService db)
        {
            _db = db;
        }

        [HttpPut]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            EnrollStudentResponse response=_db.enrollStudent(request);
            if (response == null) return BadRequest("Not valid parameters.");

            return Ok(response);
        }

        [HttpPut]
        public IActionResult PromoteStudent()
        {
            EnrollStudentResponse response = _db.enrollStudent(request);
            if (response == null) return BadRequest("Not valid parameters.");

            return Ok(response);
        }

    }
}
