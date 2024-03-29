﻿using StudentTracker.Models;
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
        private int selectedClassId;
        private string selectedClassName;
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

            selectedClassId = int.Parse(classID.Text);
            selectedClassName = classNameText.Text;

            if(course.StartDateNotificationsOn == true && course.EndDateNotificationsOn == true)
            {
                EnableNotificationsToggle.IsToggled = true;
            }
            else
            {
                EnableNotificationsToggle.IsToggled = false;
            }
        }
        async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if(!LogicCheck.IsNull(classNameText.Text)&&
                !LogicCheck.IsNull(instructorNameText.Text)&&
                !LogicCheck.IsNull(instructorEmailText.Text)&&
                !LogicCheck.IsNull(instructorPhoneText.Text))
            {
                if (LogicCheck.IsValidEmail(instructorEmailText.Text))
                {
                    if (EnableNotificationsToggle.IsToggled == true)
                    {
                        bool startNotifications = true;
                        bool endNotifications = true;

                        await DatabaseService.UpdateCourse(Int32.Parse(classID.Text), classNameText.Text, instructorNameText.Text, instructorEmailText.Text,
                        instructorPhoneText.Text, classStartDatePicker.Date, classEndDatePicker.Date, classStatusPicker.SelectedItem.ToString(), courseNotesText.Text);

                        await DatabaseService.UpdateCourseTurnOffNotifications(Int32.Parse(classID.Text), startNotifications, endNotifications);
                    }
                    if(EnableNotificationsToggle.IsToggled == false)
                    {
                        bool startNotifications = false;
                        bool endNotifications = false;

                        await DatabaseService.UpdateCourse(Int32.Parse(classID.Text), classNameText.Text, instructorNameText.Text, instructorEmailText.Text,
                       instructorPhoneText.Text, classStartDatePicker.Date, classEndDatePicker.Date, classStatusPicker.SelectedItem.ToString(), courseNotesText.Text);

                        await DatabaseService.UpdateCourseTurnOffNotifications(Int32.Parse(classID.Text), startNotifications, endNotifications);
                    }
                }
            }
            await DatabaseService.UpdateCourse(Int32.Parse(classID.Text), classNameText.Text, instructorNameText.Text, instructorEmailText.Text,
                instructorPhoneText.Text, classStartDatePicker.Date, classEndDatePicker.Date, classStatusPicker.SelectedItem.ToString(), courseNotesText.Text);

            await Navigation.PopAsync();

        }
        async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        
        async void AssessmentButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AssessmentsList(selectedClassId,selectedClassName));
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