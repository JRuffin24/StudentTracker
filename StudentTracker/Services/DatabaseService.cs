using System;
using System.IO;
using SQLite;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StudentTracker.Models;
using Xamarin.Essentials;
using System.Linq;

namespace StudentTracker.Services
{
   public static class DatabaseService
    {
        private static SQLiteAsyncConnection _db;
        private static int _newTermId;
        private static int _newClassId;
        static async Task Init()
        {
           
            if (_db != null) //Don't create database if it already exists.
            {
                return;
            }
            //get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Terms.db");

            _db = new SQLiteAsyncConnection(databasePath);

            await _db.CreateTableAsync<Term>();
            await _db.CreateTableAsync<Course>();
            await _db.CreateTableAsync<Tests>();
        }

        public static async Task DeleteAllItems()
        {
            await _db.DropTableAsync<Term>();
            await _db.DropTableAsync<Course>();
            await _db.DropTableAsync<Tests>();

            _db = null;
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
                Notes = notes,
                
            };
            var id = await _db.InsertAsync(course);
        }
        public static async Task AddAssessment(int classID, string className,string assessmentName, string assessmentType, DateTime startDate, DateTime endDate)
        {
            await Init();
            var test = new Tests
            {
                AssessmentName = assessmentName,
                ClassID = classID,
                ClassName = className,
                AssessmentType = assessmentType,
                StartDate = startDate,
                EndDate = endDate
            };
            var id = await _db.InsertAsync(test);
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
        public static async Task DeleteTest(int assessmentID)
        {
            await Init();
            await _db.DeleteAsync<Tests>(assessmentID);
        }
        
        public static async Task<IEnumerable<Tests>> GetTest(int classId)
        {
            await Init();
            var test = await _db.Table<Tests>().Where(i => i.ClassID == classId) .ToListAsync();
            return test;
        }
        public static async Task<int> ClassTotalPerTerm(int termId)
        {
            int totalClassCount = await _db.ExecuteScalarAsync<int>($"Select Count(*) from Term WHERE Id = '" + termId + "' ");
            return totalClassCount;
        }
        public static async Task<int> GetTotalTestCount(int classId)
        {
            int totalTestCount = await _db.ExecuteScalarAsync<int>($"Select Count(*) from Tests where ClassID = '{classId}'");

            return totalTestCount;
        }

        public static async Task<int> GetObjectiveTestCount(int classId)
        {
            
            string obj = "Objective Assessment";

            int objectiveTestCount = await _db.ExecuteScalarAsync<int>($"Select Count(*) from Tests WHERE ClassID = '" + classId + "' AND AssessmentType = '"+obj+"' ");


            return objectiveTestCount;
        }

        public static async Task<int> GetPerformanceTestCount(int classId)
        {
            string perf = "Performance Assessment";
            int performanceTestCount = await _db.ExecuteScalarAsync<int>($"Select Count(*) from Tests where ClassID = '" + classId + "' AND AssessmentType = '" + perf + "' ");

            return performanceTestCount;
        }

        public static async Task <int> GetCourseID(int termID, string className)
        {
            int courseID = await _db.ExecuteScalarAsync<int>($"Select ID from Course where TermID = '" + termID + "' AND CourseName = '" + className + "'");

            return courseID;
        }

        public static async Task<int> GetTermID(string termName)
        {
            int termID = await _db.ExecuteScalarAsync<int>($"Select Id from Term where TermName = '" + termName + "'");

            return termID;
        }

        public static async Task<IEnumerable<Course>> GetCourse(int termID)
        {
            await Init();
            
            var course = await _db.Table<Course>().Where(i => i.TermID == termID).ToListAsync();
            
            return course;
           
        }

        public static async Task<IEnumerable<Course>> GetCourse()
        {
            await Init();

            var courseList = await _db.Table<Course>().ToListAsync();

            return courseList;

        }
        

        public static async Task<IEnumerable<Tests>> GetTest()
        {
            await Init();
            
            var testList = await _db.Table<Tests>().ToListAsync();
            
            return testList;
        }

        public static async Task<IEnumerable<Term>> GetTerm()
        {
            await Init();
            var term = await _db.Table<Term>().ToListAsync();
            return term;
        }

        public static async Task UpdateAssessment(int assessmentID, int classID, string className, string assessmentName, string assessmentType, DateTime startDate, DateTime endDate)
        {
            await Init();
            var testQuery = await _db.Table<Tests>().Where(i => i.AssessmentID == assessmentID).FirstOrDefaultAsync();
            
            if (testQuery != null)
            {
                testQuery.AssessmentID = assessmentID;
                testQuery.ClassID = classID;
                testQuery.ClassName = className;
                testQuery.AssessmentType = assessmentType;
                testQuery.StartDate = startDate;
                testQuery.EndDate = endDate;

                await _db.UpdateAsync(testQuery);
            }
                
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

        public static async Task UpdateCourseTurnOnNotifications(int classId, Boolean startNotifications, Boolean endNotifications)
        {
            await Init();
            var courseQuery = await _db.Table<Course>()
                 .Where(i => i.Id == classId)
                 .FirstOrDefaultAsync();

            if(courseQuery != null)
            {
                startNotifications = true;
                endNotifications = true;
                courseQuery.StartDateNotificationsOn = startNotifications;
                courseQuery.EndDateNotificationsOn = endNotifications;

            }
        }

        public static async Task UpdateCourseTurnOffNotifications(int classId, Boolean startNotifications, Boolean endNotifications)
        {
            await Init();
            var courseQuery = await _db.Table<Course>()
                 .Where(i => i.Id == classId)
                 .FirstOrDefaultAsync();

            if (courseQuery != null)
            {
                startNotifications = false;
                endNotifications = false;
                courseQuery.StartDateNotificationsOn = startNotifications;
                courseQuery.EndDateNotificationsOn = endNotifications;

            }
        }

        public static async Task UpdateAssessmentTurnOffNotifications(int assessmentId, Boolean startNotifications, Boolean endNotifications)
        {
            await Init();
            var testQuery = await _db.Table<Tests>().Where(i => i.AssessmentID == assessmentId).FirstOrDefaultAsync();

            if (testQuery != null)
            {
                startNotifications = false;
                endNotifications = false;
                testQuery.StartDateNotificationsOn = startNotifications;
                testQuery.EndDateNotificationsOn = endNotifications;

                await _db.UpdateAsync(testQuery);
            }
        }

        public static async Task UpdateAssessmentTurnOnNotifications(int assessmentId, Boolean startNotifications, Boolean endNotifications)
        {
            await Init();
            var testQuery = await _db.Table<Tests>().Where(i => i.AssessmentID == assessmentId).FirstOrDefaultAsync();

            if (testQuery != null)
            {
                startNotifications = true;
                endNotifications = true;
                testQuery.StartDateNotificationsOn = startNotifications;
                testQuery.EndDateNotificationsOn = endNotifications;

                await _db.UpdateAsync(testQuery);
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

        public static async void LoadSampleData()
        {
            await Init();

            Term term = new Term
            {
                TermName = "Dummy Term",
                StartDate = DateTime.Parse("7/1/2022 12:00:00 PM"),
                EndDate = DateTime.Parse("8/31/2022 11:59:59 PM")
            };
            await _db.InsertAsync(term);

            _newTermId = term.Id; //Grabbed from term just created

            Course course = new Course
            {
                TermID = _newTermId,
                CourseName = "Mobile App Development",
                InstructorName = "Jarrett Ruffin",
                InstructorEmail = "jruff14@wgu.edu",
                InstructorPhone = "216-299-9849",
                ClassStartDate = DateTime.Parse("7/2/2022 12:00:00 PM"),
                ClassEndDate = DateTime.Parse("8/15/2022 11:59:59 PM"),
                CourseStatus = "In-Progress",
                Notes = "Mobile Apps are fun!",
                StartDateNotificationsOn = true,
                EndDateNotificationsOn = true
            };

            await _db.InsertAsync(course);

            _newClassId = course.Id;

            Tests test1 = new Tests
            {
                ClassID = _newClassId,
                ClassName = "Mobile App Development",
                AssessmentName = "test1",
                AssessmentType = "Performance Assessment",
                StartDate = DateTime.Parse("7/10/2022 12:00:00 PM"),
                EndDate = DateTime.Parse("7/12/2022 11:59:59 PM"),
                StartDateNotificationsOn = true,
                EndDateNotificationsOn = true
            };

            await _db.InsertAsync(test1);

            Tests test2 = new Tests
            {
                ClassID = _newClassId,
                ClassName = "Mobile App Development",
                AssessmentName = "test2",
                AssessmentType = "Objective Assessment",
                StartDate = DateTime.Parse("8/10/2022 12:00:00 PM"),
                EndDate = DateTime.Parse("8/12/2022 11:59:59 PM"),
                StartDateNotificationsOn = true,
                EndDateNotificationsOn = true
            };

            await _db.InsertAsync(test2);
        }


    }
}
