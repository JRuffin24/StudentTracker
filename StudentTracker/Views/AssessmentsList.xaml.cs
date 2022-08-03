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
    public partial class AssessmentsList : ContentPage
    {
        private int selectedClassID;
        private string selectedClassName;
        public AssessmentsList()
        {
            InitializeComponent();

        }
        public AssessmentsList(int classID, string className)
        {
            InitializeComponent();
            selectedClassID = classID;
            selectedClassName = className;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await DatabaseService.GetTest(selectedClassID);

            
        }
        async void AddAssessment_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddAssessment(selectedClassName,selectedClassID ));
        }
        async void CollectionView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Tests test = (Tests)e.CurrentSelection.FirstOrDefault();
                await Navigation.PushAsync(new AssessmentDetails(test));
            }
        }
    }
}