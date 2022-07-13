using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace StudentTracker.Models
{
    public class Term
    {
       
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string TermName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}