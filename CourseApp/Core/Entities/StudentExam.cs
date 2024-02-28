using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class StudentExam
    {
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public byte Point { get; set; }

        public Student Student { get; set; }
        public Exam Exam { get; set; }
    }
}
