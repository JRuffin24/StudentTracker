using StudentTracker.Models;
using StudentTracker.Services;
using StudentTracker.Views;
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
    public partial class AddTerm : ContentPage
    {
        public AddTerm()
        {
            InitializeComponent();
        }
        //public AddTerm(Term newTerm)
        //{
        //    InitializeComponent();
        //    termTitleText.Text = newTerm.TermName;
        //    startDatePicker.Date = newTerm.StartDate;
        //    EndDatePicker.Date = newTerm.EndDate;
        //}

        async void AddTermButton_Clicked(object sender, EventArgs e)
        {
            await DatabaseService.AddTerm(termTitleText.Text, startDatePicker.Date, EndDatePicker.Date);

            await Navigation.PushAsync( new MainPage());
        }

        async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}