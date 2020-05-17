using Microsoft.EntityFrameworkCore;
using s19551Assingment4.DTOs;
using s19551Assingment4.DTOs.Responce;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutorial10.DTOs.Request;
using Tutorial10.Entities;

namespace Tutorial10.Services
{
    public interface IStudentDbService
    {
        ICollection GetStudents();
        bool AddStudent(Student student);
        bool UpdateStudent(Student student);
        bool DeleteStudent(string index);
        EnrollStudentResponse enrollStudent(EnrollStudentRequest request);
        ICollection PromoteStudents(PromoteStudentRequest request);
    }
}
