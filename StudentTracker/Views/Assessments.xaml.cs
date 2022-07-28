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
    public partial class Assessments : ContentPage
    {
        public Assessments()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await DatabaseService.GetTest();
        }
        async void AddAssessment_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddAssessment());
        }
        async void CollectionView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Tests test = (Tests)e.CurrentSelection.FirstOrDefault();
                await Navigation.PushAsync(new EditAssessment(test));
            }
        }
    }
}