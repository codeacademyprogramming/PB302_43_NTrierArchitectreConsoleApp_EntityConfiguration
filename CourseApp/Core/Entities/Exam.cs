using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Exam
    {
        public int Id { get; set; }
        
        public string Subject { get; set; }
        public DateTime StartDate { get; set; }
        public List<StudentExam> StudentExams { get; set; }
    }
}
