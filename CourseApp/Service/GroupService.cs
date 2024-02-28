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
    public class GroupService
    {
        private CourseDbContext _context;
        public GroupService()
        {
            _context = new CourseDbContext();
        }
        public void Create(Group entity)
        {
            if(_context.Groups.Any(x=>x.No == entity.No))
                throw new EntityDublicateException("Group already exists by no: " + entity.No);

            _context.Groups.Add(entity);
            _context.SaveChanges();
        }

        public Group GetById(int id)
        {
            var entity = _context.Groups.FirstOrDefault(x => x.Id == id);

            if (entity == null) throw new EntityNotFoundException("Group not found");
            return entity;
        }

        public List<Group> GetAll()
        {
            return _context.Groups.Include(x=>x.Students).ToList();
        }
        public void Delete(int id)
        {
            var entity = _context.Groups.FirstOrDefault(x => x.Id == id);

            if (entity == null) throw new EntityNotFoundException("Group not found");

            _context.Groups.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(int id,Group entity)
        {
            var existEntity = _context.Groups.FirstOrDefault(x => x.Id == id);

            if (existEntity == null) throw new EntityNotFoundException("Group not found");

            existEntity.No = entity.No;
            existEntity.Limit = entity.Limit;
            existEntity.StartDate = entity.StartDate;

            _context.SaveChanges();
        }

        public bool ExistsById(int id)
        {
            return _context.Groups.Any(x => x.Id == id);
        }
    }
}
