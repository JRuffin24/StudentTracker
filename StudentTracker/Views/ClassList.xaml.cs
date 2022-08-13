using StudentTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentTracker.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTracker.Models;

namespace StudentTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClassList : ContentPage
    {

        private int selectedTermID;
        
        public ClassList()
        {
            InitializeComponent();
        }
        public ClassList(int termID)
        {
            InitializeComponent();
            selectedTermID = termID;

            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //int termId = await DatabaseService.GetTermID(dummyTerm.TermName);
            //var dummyCourse = new Course();
            //dummyCourse.TermID = 1;
            //dummyCourse.Id = 1;
            //dummyCourse.CourseName = "Mobile Application Development";
            //dummyCourse.InstructorName = "Jarrett Ruffin";
            //dummyCourse.InstructorPhone = "216-299-9849";
            //dummyCourse.InstructorEmail = "jruff14@wgu.edu";
            //dummyCourse.ClassStartDate = new DateTime(2022, 06, 01);
            //dummyCourse.ClassEndDate = new DateTime(2022, 08, 31);
            //dummyCourse.CourseStatus = "In-Progress";
            //dummyCourse.Notes = "Mobile Apps are fun!";
            //dummyCourse.StartDateNotificationsOn = true;
            //dummyCourse.EndDateNotificationsOn = true;
            //await DatabaseService.AddCourse(dummyCourse.TermID, dummyCourse.CourseName, dummyCourse.InstructorName, dummyCourse.InstructorEmail
            //    , dummyCourse.InstructorPhone, dummyCourse.ClassStartDate, dummyCourse.ClassEndDate, dummyCourse.CourseStatus, dummyCourse.Notes);

            ClassCollectionView.ItemsSource = await DatabaseService.GetCourse(selectedTermID);
        }
        async void AddClass_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddClass(selectedTermID));
        }
        async void CollectionView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Course course = (Course)e.CurrentSelection.FirstOrDefault();
                await Navigation.PushAsync(new EditClass(course));
            }
        }
    }
}