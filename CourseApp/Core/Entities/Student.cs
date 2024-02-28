using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public byte Point { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public List<StudentExam> StudentExams { get; set; }
    }
}
