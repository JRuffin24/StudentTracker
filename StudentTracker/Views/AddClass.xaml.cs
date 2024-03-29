﻿using StudentTracker.Models;
using StudentTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddClass : ContentPage
    {

        private int selectedTermID;
        
        public AddClass()
        {
            InitializeComponent();
        }
        public AddClass(int termID, string className)
        {
            InitializeComponent();

            selectedTermID = termID;            
            TermID.Text = selectedTermID.ToString();
        }
        async void AddClassButton_Clicked(object sender, EventArgs e)
        {
            int totalClassCount = await DatabaseService.ClassTotalPerTerm(selectedTermID);

            //Logic checks to make sure fields are not left blank, the email address is valid, start date is before end date and the total class count is
            //less than six

            if (!LogicCheck.IsNull(classNameText.Text) || 
                !LogicCheck.IsNull(instructorNameText.Text) ||
                !LogicCheck.IsNull(instructorEmailText.Text) ||
                !LogicCheck.IsNull(instructorPhoneText.Text))
            {
                if (LogicCheck.IsValidEmail(instructorEmailText.Text))
                {
                    if(classStartDatePicker.Date < classEndDatePicker.Date)
                    {
                        if(totalClassCount >= 6)
                        {
                            await DisplayAlert("Error.", "This term has reached the maximum number of courses. If you wish to add another class to this term, delete a existing class.", "Ok");
                            return;
                        }
                        else
                        {   
                            if(EnableNotificationsToggle.IsToggled == true)
                            {
                                bool startNotifications = true;
                                bool endNotifications = true;

                                await DatabaseService.AddCourse(Int32.Parse(TermID.Text), classNameText.Text, instructorNameText.Text, instructorEmailText.Text, 
                                instructorPhoneText.Text, classStartDatePicker.Date,
                                classEndDatePicker.Date, classStatusPicker.SelectedItem.ToString(), courseNotesText.Text);

                                int courseId = await DatabaseService.GetCourseID(Int32.Parse(TermID.Text), classNameText.Text);

                                await DatabaseService.UpdateCourseTurnOnNotifications(courseId, startNotifications, endNotifications);
                            }
                            if(EnableNotificationsToggle.IsToggled == false)
                            {
                                bool startNotifications = false;
                                bool endNotifications = false;

                                await DatabaseService.AddCourse(Int32.Parse(TermID.Text), classNameText.Text, instructorNameText.Text, instructorEmailText.Text,
                                instructorPhoneText.Text, classStartDatePicker.Date,
                                classEndDatePicker.Date, classStatusPicker.SelectedItem.ToString(), courseNotesText.Text);

                                int courseId = await DatabaseService.GetCourseID(Int32.Parse(TermID.Text), classNameText.Text);

                                await DatabaseService.UpdateCourseTurnOffNotifications(courseId, startNotifications, endNotifications);
                            }
            
                            await Navigation.PushAsync(new ClassList(selectedTermID));
                        }
                        
                        
                    }
                    else await DisplayAlert("Error.", "Please ensure start date is before end date.", "Ok");
                }
                else await DisplayAlert("Error.", "Please ensure all fields are completed.", "Ok");
            }
            else await DisplayAlert("Error.", "Please provide a valid email address", "Ok");

        }
        async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        
    }
}