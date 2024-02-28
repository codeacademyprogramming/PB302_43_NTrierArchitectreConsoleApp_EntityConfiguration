using Core.Entities;
using Data;
using Microsoft.EntityFrameworkCore;
using Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ExamService
    {
        private CourseDbContext _context;

        public ExamService()
        {
            _context = new CourseDbContext();
        }

        public void Create(Exam entity)
        {
            _context.Exams.Add(entity);
            _context.SaveChanges();
        }

        public Exam GetById(int id)
        {
            var entity = _context.Exams.Include(x => x.StudentExams).FirstOrDefault(x=>x.Id == id);

            if (entity == null) throw new EntityNotFoundException("Exam not found");

            return entity;
        }

        public void AddStudentResult(StudentExam studentExam)
        {

            _context.StudentExams.Add(studentExam);
            _context.SaveChanges();
        }

        public List<Exam> GetAll()
        {
            return _context.Exams.Include(x => x.StudentExams).ThenInclude(x => x.Student).ToList();
        }

    }
}
