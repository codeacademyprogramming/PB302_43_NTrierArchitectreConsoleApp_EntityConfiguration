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
    public class StudentService
    {
        private CourseDbContext _context;

        public StudentService()
        {
            _context = new CourseDbContext();
        }

        public void Create(Student entity)
        {
            Group group = _context.Groups.Include(x=>x.Students).FirstOrDefault(x=>x.Id == entity.GroupId);
          
            if (group == null) throw new EntityNotFoundException("Group not found");

            if (group.Students.Count >= group.Limit) throw new GroupLimitException();

            _context.Students.Add(entity);
            _context.SaveChanges();
        }

        public Student GetById(int id)
        {
            Student entity = _context.Students.Include(x => x.Group).FirstOrDefault(x => x.Id == id);

            if (entity == null) throw new EntityNotFoundException("Student not found");

            return entity;
        }

        public List<Student> GetAll()
        {
            return _context.Students.Include(x => x.Group).ToList();
        }

        public void Update(int id, Student entity)
        {
            Student existEntity = _context.Students.Find(id);

            if (existEntity == null) throw new EntityNotFoundException("Student not found");

            if(entity.GroupId!=existEntity.GroupId)
            {
                var group = _context.Groups.Include(x => x.Students).FirstOrDefault(x => x.Id == entity.GroupId);

                if (group == null) throw new EntityNotFoundException("Group not found");

                if (group.Limit <= group.Students.Count) throw new GroupLimitException();
            }

            existEntity.GroupId = entity.GroupId;
            existEntity.Point = entity.Point;
            existEntity.Fullname = entity.Fullname;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Student entity = _context.Students.Find(id);

            if (entity == null) throw new EntityNotFoundException("Student not found");

            _context.Students.Remove(entity);
            _context.SaveChanges();
        }
    }
}
