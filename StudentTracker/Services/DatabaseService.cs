using System;
using System.IO;
using SQLite;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StudentTracker.Models;

namespace StudentTracker.Services
{
   public static class DatabaseService
    {
        private static SQLiteAsyncConnection _db;
        static async Task Init()
        {
           
            if (_db != null) //Don't create database if it already exists.
            {
                return;
            }
            //get an absolute path to the database file
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Terms.db");

            _db = new SQLiteAsyncConnection(databasePath);

            await _db.CreateTableAsync<Term>();
            await _db.CreateTableAsync<Course>();
        }
        public static async Task AddTerm(string termName, DateTime start, DateTime end)
        {
            await Init();
            var term = new Term
            {
                TermName = termName,
                StartDate = start,
                EndDate = end
            };
            var id = await _db.InsertAsync(term);
        }
        public static async Task AddCourse(int termID, string courseName, string instructorName, string instructorEmail, string instructorPhone, DateTime classStart, DateTime classEnd, string courseStatus, string notes)
        {
            await Init();
            var course = new Course
            {   
                TermID = termID,
                CourseName = courseName,
                InstructorName = instructorName,
                InstructorPhone = instructorPhone,
                InstructorEmail = instructorEmail,
                ClassStartDate = classStart,
                ClassEndDate = classEnd,
                CourseStatus = courseStatus,
                Notes = notes
            };
            var id = await _db.InsertAsync(course);
        }

        public static async Task DeleteCourse(int courseID)
        {
            await Init();
            await _db.DeleteAsync<Course>(courseID);
        }
        public static async Task DeleteTerm(int termID)
        {
            await Init();

            await _db.DeleteAsync<Term>(termID);
        }
    
        public static async Task<IEnumerable<Course>> GetCourse()
        {
            await Init();
            var course = await _db.Table<Course>().ToListAsync();
            return course;
        }
        public static async Task<IEnumerable<Term>> GetTerm()
        {
            await Init();
            var term = await _db.Table<Term>().ToListAsync();
            return term;
        }

        public static async Task UpdateCourse(int courseID, string courseName, string instructorName, string instructorEmail, string instructorPhone, DateTime classStart, DateTime classEnd, string courseStatus, string notes)
        {
            await Init();

            var courseQuery = await _db.Table<Course>()
                .Where(i => i.Id == courseID)
                .FirstOrDefaultAsync();

            if (courseQuery != null)
            {
                courseQuery.CourseName = courseName;
                courseQuery.InstructorName = instructorName;
                courseQuery.InstructorEmail = instructorEmail;
                courseQuery.InstructorPhone = instructorPhone;
                courseQuery.ClassStartDate = classStart;
                courseQuery.ClassEndDate = classEnd;

                await _db.UpdateAsync(courseQuery);
            }
        }

        public static async Task UpdateTerm (int termID, string termName, DateTime start, DateTime end)
        {
            await Init();

            var termQuery = await _db.Table<Term>()
                .Where(i => i.Id == termID)
                .FirstOrDefaultAsync();

            if(termQuery != null)
            {
                termQuery.TermName = termName;
                termQuery.StartDate = start;
                termQuery.EndDate = end;

                await _db.UpdateAsync(termQuery);
            }
        }

    }
}
