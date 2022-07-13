using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.Models
{
     public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int TermID { get; set; }
        public string CourseName { get; set; }
        public string InstructorName { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }
        public DateTime ClassStartDate { get; set; }
        public DateTime ClassEndDate { get; set; }
        public string CourseStatus { get; set; }
        public string Notes { get; set; }
    }
}
