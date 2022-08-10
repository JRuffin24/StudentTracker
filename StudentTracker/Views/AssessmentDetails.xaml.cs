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
    public partial class AssessmentDetails : ContentPage
    {
        
        public AssessmentDetails()
        {
            InitializeComponent();
        }
        public AssessmentDetails(Tests test)
        {
            InitializeComponent();
            ClassID.Text = test.ClassID.ToString();
            AssessmentID.Text = test.AssessmentID.ToString();
            AssessmentName.Text = test.AssessmentName;
            ClassNameText.Text = test.ClassName;
            Assessment1TypePicker.SelectedItem = test.AssessmentType;
            TestStartDatePicker.Date = test.StartDate;
            TestEndDatePicker.Date = test.EndDate;

           
        }

        async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if(LogicCheck.IsNull(AssessmentName.Text)&&
                LogicCheck.IsNull(Assessment1TypePicker.SelectedItem.ToString()))
            {
                if(TestStartDatePicker.Date < TestEndDatePicker.Date)
                {
                    if (EnableNotificationsToggle.IsToggled == true)
                    {
                        bool startNotifications = true;
                        bool endNotifications = true;

                        await DatabaseService.UpdateAssessment(Int32.Parse(AssessmentID.Text), Int32.Parse(ClassID.Text), ClassNameText.Text, AssessmentName.Text, Assessment1TypePicker.SelectedItem.ToString(), TestStartDatePicker.Date,
                        TestEndDatePicker.Date);

                        await DatabaseService.UpdateAssessmentTurnOnNotifications(AssessmentName.Text, startNotifications, endNotifications);
                        
                    }

                    if(EnableNotificationsToggle.IsToggled == false)
                    {
                        bool startNotifications = true;
                        bool endNotifications = true;

                        await DatabaseService.UpdateAssessment(Int32.Parse(AssessmentID.Text), Int32.Parse(ClassID.Text), ClassNameText.Text, AssessmentName.Text, Assessment1TypePicker.SelectedItem.ToString(), TestStartDatePicker.Date,
                        TestEndDatePicker.Date);

                        await DatabaseService.UpdateAssessmentTurnOffNotifications(AssessmentName.Text, startNotifications, endNotifications);
                        await Navigation.PopAsync();
                    }
                    await Navigation.PopAsync();
                }
                else await DisplayAlert("Error.", "Please ensure start date is before end date.", "Ok");
                return;
            }
            else await DisplayAlert("Error.", "Please ensure all fields are completed.", "Ok");
            return;

        }
        async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var confirmation = await DisplayAlert("Alert", "Are you sure you want to drop this Assessment?", "Yes", "No");
            if(confirmation)
            {
                var id = int.Parse(AssessmentID.Text);

                await DatabaseService.DeleteTest(id);

                await Navigation.PopAsync();
            }
           
        }
        async void CancelButton_Clicked (object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        
    }
}