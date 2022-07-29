using StudentTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentTracker.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTracker.Models;

namespace StudentTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Classes : ContentPage
    {

        private int selectedTermID;
        public Classes()
        {
            InitializeComponent();
        }
        public Classes(int termID)
        {
            InitializeComponent();
            selectedTermID = termID;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ClassCollectionView.ItemsSource = await DatabaseService.GetCourse();
        }
        async void AddClass_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddClass(selectedTermID));
        }
        async void CollectionView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Course course = (Course)e.CurrentSelection.FirstOrDefault();
                await Navigation.PushAsync(new EditClass(course));
            }
        }
    }
}