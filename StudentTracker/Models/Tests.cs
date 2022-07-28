using SQLite;
using System;
using System.Collections.Generic;
using System.Text;


namespace StudentTracker.Models
{
    public class Tests
    {
        [PrimaryKey, AutoIncrement]        
        public int AssessmentID { get; set; }
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public string AssessmentType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
} 
