using SQLite;
using StudentTracker.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAssessment : ContentPage
    {
        private string SelectedClassName;
        private int selectedClassId;
        public AddAssessment()
        {
            InitializeComponent();
            
        }
        public AddAssessment(string cName, int cID)
        {
            InitializeComponent();
            SelectedClassName = cName;
            selectedClassId = cID;
            ClassName.Text = SelectedClassName;
            ClassID.Text = selectedClassId.ToString();
        }



        async void AddAssessmentButton_Clicked(object sender, EventArgs e)
        {
            int totalTestCount = await DatabaseService.GetTotalTestCount(selectedClassId);
            int objectiveCount = await DatabaseService.GetObjectiveTestCount(selectedClassId);
            int performanceCount = await DatabaseService.GetPerformanceTestCount(selectedClassId);
            
            //Logic check to make sure Assessment name is not empty.
            if (LogicCheck.IsNull(AssessmentName.Text))
            {    //Logic check to make sure start date is before end date.
                if (TestStartDatePicker.Date < TestEndDatePicker.Date)
                {   //Logic check to make sure the total test count is not larger 2
                    if (totalTestCount >= 2)
                    {
                        await DisplayAlert("Error.", "This course has reached the maximum number of assessments. If you wish to add another test, delete a existing assessment.", "Ok");
                        return;
                    }
                    else
                    {
                        if (Assessment1TypePicker.SelectedItem == "Objective Assessment")
                        {
                            if (objectiveCount == 1)
                            {
                                await DisplayAlert("Error.", "This course has reached the maximum number of Objective assessments. If you wish to add another test, delete the existing Objective assessment.", "Ok");
                                return;
                            }
                            else
                            {
                                await DatabaseService.AddAssessment(int.Parse(ClassID.Text), ClassName.Text, AssessmentName.Text, Assessment1TypePicker.SelectedItem.ToString(),
                                TestStartDatePicker.Date, TestEndDatePicker.Date);
                            }
                        }
                        if (Assessment1TypePicker.SelectedItem == "Performance Assessment")
                        {
                            if (performanceCount == 1)
                            {
                                await DisplayAlert("Error.", "This course has reached the maximum number of Performance assessments. If you wish to add another test, delete the existing Performance assessment.", "Ok");
                                return;
                            }
                            else
                            {
                                await DatabaseService.AddAssessment(int.Parse(ClassID.Text), ClassName.Text, AssessmentName.Text, Assessment1TypePicker.SelectedItem.ToString(),
                                TestStartDatePicker.Date, TestEndDatePicker.Date);
                            }
                        }

                        await Navigation.PushAsync(new AssessmentsList(selectedClassId,SelectedClassName));
                    }
                }

                else await DisplayAlert("Error.", "Please ensure start date is before end date.", "Ok");
            }
            else await DisplayAlert("Error.", "Please ensure all fields are completed.", "Ok");

        }
        async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}