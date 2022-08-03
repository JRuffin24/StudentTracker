using StudentTracker.Models;
using StudentTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditClass : ContentPage
    {
        public EditClass()
        {
            InitializeComponent();
        }
        public EditClass(Course course)
        {
            InitializeComponent();

            termID.Text = course.TermID.ToString();
            classID.Text = course.Id.ToString();
            classNameText.Text = course.CourseName;
            instructorNameText.Text = course.InstructorName;
            instructorPhoneText.Text = course.InstructorPhone;
            instructorEmailText.Text = course.InstructorEmail;
            classStartDatePicker.Date = course.ClassStartDate;
            classEndDatePicker.Date = course.ClassEndDate;
            classStatusPicker.SelectedItem = course.CourseStatus;
            courseNotesText.Text = course.Notes;
        }
        async void SaveButton_Clicked(object sender, EventArgs e)
        {
            await DatabaseService.UpdateCourse(Int32.Parse(classID.Text), classNameText.Text, instructorNameText.Text, instructorEmailText.Text,
                instructorPhoneText.Text, classStartDatePicker.Date, classEndDatePicker.Date, classStatusPicker.SelectedItem.ToString(), courseNotesText.Text);

            await Navigation.PopAsync();

        }
        async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private void Notifications_OnToggle(object sender, EventArgs e)
        {

        }
        async void AssessmentButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AssessmentsList());
        }
        async void DeleteButton_Clicked(object sender, EventArgs e)
        {

            var id = int.Parse(classID.Text);

            await DatabaseService.DeleteCourse(id);

            await Navigation.PopAsync();
        }

        async void ShareButton_Clicked(object sender, EventArgs e)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = courseNotesText.Text,
                Title = "Share your notes from this course!"

            });
        }
    }
}