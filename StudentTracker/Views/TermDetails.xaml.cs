using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentTracker.Models;
using StudentTracker.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermDetails : ContentPage
    {

        private int selectedTermID;
        
        public TermDetails()
        {
            InitializeComponent();
        }
        public TermDetails(Term selectedTerm)
        {
            InitializeComponent();
            //Populate controls
            termID.Text = selectedTerm.Id.ToString();
            termTitle.Text = selectedTerm.TermName;
            startDatePicker.Date = selectedTerm.StartDate;
            EndDatePicker.Date = selectedTerm.EndDate;
            selectedTermID = selectedTerm.Id;
        }
        async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (!LogicCheck.IsNull(termTitle.Text))
            {
                if(startDatePicker.Date < EndDatePicker.Date)
                {
                    await DatabaseService.UpdateTerm(Int32.Parse(termID.Text), termTitle.Text, startDatePicker.Date, EndDatePicker.Date);

                    await Navigation.PopAsync();
                }
                else await DisplayAlert("Error.", "Please ensure start date is before end date.", "Ok");
            }
            else await DisplayAlert("Error.", "Please ensure all fields are filled in.", "Ok");
        }
        async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var id = int.Parse(termID.Text);

            await DatabaseService.DeleteTerm(id);

            await Navigation.PopAsync();
        }

        async void ClassList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClassList(selectedTermID));
        }

    }
}