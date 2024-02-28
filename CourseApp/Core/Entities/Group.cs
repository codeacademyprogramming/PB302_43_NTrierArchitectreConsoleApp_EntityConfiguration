﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string No { get; set; }
        public int Limit { get; set; }
        public DateTime StartDate { get; set; }
        public List<Student> Students { get; set; }
    }
}
