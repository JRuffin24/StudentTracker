﻿using System;
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
    public partial class EditTerm : ContentPage
    {
        public EditTerm()
        {
            InitializeComponent();
        }
        public EditTerm(Term selectedTerm)
        {
            InitializeComponent();
            //Populate controls
            termID.Text = selectedTerm.Id.ToString();
            termTitle.Text = selectedTerm.TermName;
            startDatePicker.Date = selectedTerm.StartDate;
            EndDatePicker.Date = selectedTerm.EndDate;
        }
        async void SaveButton_Clicked(object sender, EventArgs e)
        {
            await DatabaseService.UpdateTerm(Int32.Parse(termID.Text), termTitle.Text, startDatePicker.Date, EndDatePicker.Date);

            await Navigation.PopAsync();
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
       
    }
}