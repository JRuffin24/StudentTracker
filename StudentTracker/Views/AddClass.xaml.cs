using StudentTracker.Models;
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
        public AddClass(int termID)
        {
            InitializeComponent();

            selectedTermID = termID;
        }
        async void AddClassButton_Clicked(object sender, EventArgs e)
        {
            if (LogicCheck.IsNull(classNameText.Text) && 
                LogicCheck.IsNull(instructorNameText.Text) &&
                LogicCheck.IsNull(instructorEmailText.Text) &&
                LogicCheck.IsNull(instructorPhoneText.Text))
            {
                if (LogicCheck.IsValidEmail(instructorEmailText.Text))
                {
                    if(classStartDatePicker.Date < classEndDatePicker.Date)
                    {
                        await DatabaseService.AddCourse(Int32.Parse(TermID.Text), classNameText.Text, instructorNameText.Text, instructorEmailText.Text, 
                            instructorPhoneText.Text, classStartDatePicker.Date,
                        classEndDatePicker.Date, classStatusPicker.SelectedItem.ToString(), courseNotesText.Text);
            
                        await Navigation.PushAsync(new ClassList());
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
         async void Notifications_OnToggle(object sender, EventArgs e)
        {

        }
    }
}