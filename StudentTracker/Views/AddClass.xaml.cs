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

            TermID.Text = termID.ToString();
        }
        async void AddClassButton_Clicked(object sender, EventArgs e)
        {
            await DatabaseService.AddCourse(Int32.Parse(TermID.Text), classNameText.Text, instructorNameText.Text, instructorEmailText.Text, instructorPhoneText.Text, classStartDatePicker.Date,
                classEndDatePicker.Date, classStatusPicker.SelectedItem.ToString(), courseNotesText.Text);
            
            await Navigation.PushAsync(new Classes());
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