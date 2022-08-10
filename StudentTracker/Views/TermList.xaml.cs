using Plugin.LocalNotifications;
using StudentTracker.Models;
using StudentTracker.Services;
using StudentTracker.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public bool pushNotification = true;
        public  MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await DatabaseService.GetTerm();
            var courseList = await DatabaseService.GetCourse();
            var testList = await DatabaseService.GetTest();
            var termList = await DatabaseService.GetTerm();
            //notification handling
            if (pushNotification == true)
            {
                pushNotification = false;
                int courseID = 0;
                foreach(Course course in courseList)
                {
                    courseID++;

                    if(course.StartDateNotificationsOn == true && course.EndDateNotificationsOn == true)
                    {
                        if(course.ClassStartDate == DateTime.Today)
                        {
                            CrossLocalNotifications.Current.Show("Reminder", $"{course.CourseName} from TermID {course.TermID} Begins today!", courseID);
                        }
                        if(course.ClassEndDate == DateTime.Today)
                        {
                            CrossLocalNotifications.Current.Show("Reminder", $"{course.CourseName} from TermID {course.TermID} Ends today!", courseID);
                        }
                    }
                }
                int assessmentId = courseID;

                foreach(Tests assessment in testList)
                {
                    assessmentId++;

                    if(assessment.StartDateNotificationsOn == true && assessment.EndDateNotificationsOn == true)
                    {
                        if(assessment.StartDate == DateTime.Today)
                        {
                            CrossLocalNotifications.Current.Show("Reminder", $" Assessment for {assessment.ClassName} with the name {assessment.AssessmentName} Begins today!", courseID);
                        }
                        if(assessment.EndDate == DateTime.Today)
                        {
                            CrossLocalNotifications.Current.Show("Reminder", $" Assessment for {assessment.ClassName} with the name {assessment.AssessmentName} Ends today!", courseID);
                        }
                    }
                }
            }

            if (!termList.Any())
            {
                var dummyTerm = new Term();
                dummyTerm.Id = 1;
                dummyTerm.TermName = "Dummy Term";
                dummyTerm.StartDate = new DateTime(2022, 08, 15);
                dummyTerm.EndDate = new DateTime(2022, 08, 30);
                await DatabaseService.AddTerm(dummyTerm.TermName, dummyTerm.StartDate, dummyTerm.EndDate);

                var dummyCourse = new Course();
                dummyCourse.TermID = 1;
                dummyCourse.Id = 1;
                dummyCourse.CourseName = "Mobile Application Development";
                dummyCourse.InstructorName = "Jarrett Ruffin";
                dummyCourse.InstructorPhone = "216-299-9849";
                dummyCourse.InstructorEmail = "jruff14@wgu.edu";
                dummyCourse.ClassStartDate = new DateTime(2022, 06, 01);
                dummyCourse.ClassEndDate = new DateTime(2022, 08, 31);
                dummyCourse.CourseStatus = "In-Progress";
                dummyCourse.Notes = "Mobile Apps are fun!";
                dummyCourse.StartDateNotificationsOn = true;
                dummyCourse.EndDateNotificationsOn = true;
                await DatabaseService.AddCourse(dummyCourse.TermID, dummyCourse.CourseName, dummyCourse.InstructorName, dummyCourse.InstructorEmail
                    , dummyCourse.InstructorPhone, dummyCourse.ClassStartDate, dummyCourse.ClassEndDate, dummyCourse.CourseStatus, dummyCourse.Notes);
                await DatabaseService.UpdateCourseTurnOnNotifications(dummyCourse.Id, dummyCourse.StartDateNotificationsOn, dummyCourse.EndDateNotificationsOn);

                var dummyAssessment = new Tests();
                dummyAssessment.AssessmentID = 1;
                dummyAssessment.ClassID = 1;
                dummyAssessment.ClassName = "Mobile Application Development";
                dummyAssessment.AssessmentName = "Assessment2";
                dummyAssessment.AssessmentType = "Performance Assessment";
                dummyAssessment.StartDate = new DateTime(2022, 06, 10);
                dummyAssessment.EndDate = new DateTime(2022, 07, 10);
                dummyAssessment.StartDateNotificationsOn = true;
                dummyAssessment.EndDateNotificationsOn = true;
                await DatabaseService.AddAssessment(dummyAssessment.ClassID, dummyAssessment.ClassName, dummyAssessment.AssessmentName,
                   dummyAssessment.AssessmentType, dummyAssessment.StartDate, dummyAssessment.EndDate);
                await DatabaseService.UpdateAssessmentTurnOnNotifications(dummyAssessment.AssessmentID, dummyAssessment.StartDateNotificationsOn,
                    dummyAssessment.EndDateNotificationsOn);


                var dummyAssessment2 = new Tests();
                dummyAssessment2.AssessmentID = 2;
                dummyAssessment2.ClassID = 1;
                dummyAssessment2.ClassName = "Mobile Application Development";
                dummyAssessment2.AssessmentName = "Assessment1";
                dummyAssessment2.AssessmentType = "Objective Assessment";
                dummyAssessment2.StartDate = new DateTime(2022, 07, 10);
                dummyAssessment2.EndDate = new DateTime(2022, 08, 10);
                dummyAssessment2.StartDateNotificationsOn = true;
                dummyAssessment2.EndDateNotificationsOn = true;
                await DatabaseService.AddAssessment(dummyAssessment2.ClassID, dummyAssessment2.ClassName, dummyAssessment2.AssessmentName,
                   dummyAssessment2.AssessmentType, dummyAssessment2.StartDate, dummyAssessment2.EndDate);
                await DatabaseService.UpdateAssessmentTurnOnNotifications(dummyAssessment2.AssessmentID, dummyAssessment2.StartDateNotificationsOn,
                    dummyAssessment2.EndDateNotificationsOn);
                
            }


        }

        async void AddTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTerm());
        }

        async void CollectionView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.CurrentSelection != null)
            {
                Term term = (Term)e.CurrentSelection.FirstOrDefault();
                await Navigation.PushAsync(new TermDetails(term));
            }
        }
       
        
    }
}