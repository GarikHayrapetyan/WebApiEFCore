using Microsoft.EntityFrameworkCore;
using s19551Assingment4.DTOs;
using s19551Assingment4.DTOs.Responce;
using System;
using System.Collections;
using System.Linq;
using Tutorial10.DTOs.Request;
using Tutorial10.Entities;
using Tutorial10.Model;

namespace Tutorial10.Services
{
    public class StudentDbService : IStudentDbService
    {
        private readonly StudentContext context;

        public StudentDbService(StudentContext context)
        {
            this.context = context;
        }

        public bool AddStudent(Student student)
        {
            var index = student.IndexNumber;

            if (!context.Student.Any(s => s.IndexNumber == index))
            {
                context.Student.Add(student);
                context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool DeleteStudent(string index)
        {
            var student = context.Student.FirstOrDefault(s=>s.IndexNumber==index);
            if (student != null)
            {
                context.Student.Remove(student);
                context.SaveChanges();
                return true;
            }

            return false;
        }

        public ICollection GetStudents()
        {
            return context.Student
                .Include(s=>s.IdEnrollmentNavigation)
                .ThenInclude(s=>s.IdStudyNavigation)
                .Select(st=>new GetStudentsResponse {
                        IndexNumber=st.IndexNumber,
                        FirstName=st.FirstName,
                        LastName=st.LastName,
                        BirthDate=st.BirthDate,
                        IdEnrollment=st.IdEnrollment,
                        Study = st.IdEnrollmentNavigation.IdStudyNavigation.Name
        }).ToList();

        }

        public bool UpdateStudent(Student student)
        {
            var index = student.IndexNumber;
            var idEnroll = student.IdEnrollment;
            Student st = context.Student.FirstOrDefault(s => s.IndexNumber == index);
            Enrollment enroll = context.Enrollment.FirstOrDefault(e=>e.IdEnrollment == idEnroll);

            if (st!=null && enroll!=null)
            {
                st.FirstName = student.FirstName;
                st.LastName = student.LastName;
                st.BirthDate = student.BirthDate;
                st.IdEnrollment = student.IdEnrollment;
                context.SaveChanges();
                return true;                
            }
            return false;
        }

        public EnrollStudentResponse enrollStudent(EnrollStudentRequest request)
        {
            if (context.Student.Any(s => s.IndexNumber == request.IndexNumber)) return null;


            var study = request.Study;
            bool isStudyExists = context.Studies.Any(s=>s.Name==study);           

            if (!isStudyExists) return null;


            var idStudy = context.Studies.FirstOrDefault(s => s.Name == study).IdStudy;
            DateTime dt = context.Enrollment.Where(e => e.IdStudy == idStudy && e.Semester == 1).Max(d => d.StartDate);

            Enrollment enrollment = context.Enrollment
                .FirstOrDefault(e => e.IdStudy == idStudy 
                && e.Semester==1 && e.StartDate==dt);

            if (enrollment==null)
            {
                int id = context.Enrollment.Max(e => e.IdEnrollment) + 1;
                enrollment = new Enrollment {
                    IdEnrollment = id, 
                    Semester=1,
                    IdStudy=idStudy,StartDate=DateTime.Now
                };
                context.Enrollment.Add(enrollment);
                context.SaveChanges();
            }


            var student = new Student
            {
                IndexNumber=request.IndexNumber,
                FirstName = request.Name,
                LastName = request.Surname,
                BirthDate = request.Birthdate,
                IdEnrollment = enrollment.IdEnrollment
            };

            context.Student.Add(student);
            context.SaveChanges();

            return new EnrollStudentResponse
            {
                IdEnrollment = enrollment.IdEnrollment,
                Semester = enrollment.Semester,
                StartDate = enrollment.StartDate,
                IdStudy = idStudy
            };

        }

        //I am not sure about calling procedure from database
        public ICollection PromoteStudents(PromoteStudentRequest request)
        {
          return context.Enrollment.FromSql($"PromoteStudent '{request.Study}' {request.Semester}").ToList();
        }
    }
}
