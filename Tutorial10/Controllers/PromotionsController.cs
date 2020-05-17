using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutorial10.DTOs.Request;
using Tutorial10.Services;

namespace Tutorial10.Controllers
{
    [ApiController]
    [Route("api/promotion")]
    public class PromotionsController : ControllerBase
    {
        private readonly IStudentDbService _db;

        public PromotionsController (IStudentDbService db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult PromoteStudent(PromoteStudentRequest request)
        {
            return Ok(_db.PromoteStudents(request));
        }


    }
}
