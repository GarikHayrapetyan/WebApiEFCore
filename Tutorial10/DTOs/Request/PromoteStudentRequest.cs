using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tutorial10.DTOs.Request
{
    public class PromoteStudentRequest
    {
        public int Semester { get; set; }
        public string Study{ get; set; }
    }
}
