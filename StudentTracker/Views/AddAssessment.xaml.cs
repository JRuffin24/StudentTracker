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
        private string className;
        private static SQLiteConnection dbSync;
        public AddAssessment()
        {
            InitializeComponent();
            
        }
        public AddAssessment(string cName)
        {
            InitializeComponent();
            className = cName;
        }



        async void AddAssessmentButton_Clicked(object sender, EventArgs e)
        {
            if (LogicCheck.IsNull(AssessmentName.Text))
            {
                if(TestStartDatePicker.Date < TestEndDatePicker.Date)
                {
                    await DatabaseService.GetTestCount(int.Parse(ClassID.Text));
                   
                        await DatabaseService.AddAssessment(int.Parse(ClassID.Text), ClassName.Text, AssessmentName.Text, Assessment1TypePicker.SelectedItem.ToString(), 
                        TestStartDatePicker.Date, TestEndDatePicker.Date);

                        await Navigation.PushAsync(new AssessmentsList());
                   // }
                   
                }
                else await DisplayAlert("Error.", "Please ensure start date is before end date.", "Ok");
            }
            else await DisplayAlert("Error.", "Please ensure all fields are completed.", "Ok");

        }
        async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private void Notifications_OnToggle(object sender, EventArgs e)
        {

        }
    }
}