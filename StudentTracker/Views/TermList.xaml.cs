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
                dummyTerm.TermName = "Dummy Term";
                dummyTerm.StartDate = new DateTime(2022, 08, 15);
                dummyTerm.EndDate = new DateTime(2022, 08, 30);
                await DatabaseService.AddTerm(dummyTerm.TermName, dummyTerm.StartDate, dummyTerm.EndDate);

                var dummyCourse = new Course();
                dummyCourse.CourseName = "Mobile Application Development";
                dummyCourse.InstructorName = "Jarrett Ruffin";
                dummyCourse.InstructorPhone = "216-299-9849";
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