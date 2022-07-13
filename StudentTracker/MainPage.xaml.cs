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

namespace StudentTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public  MainPage()
        {
            Term term = new Term();
            Course course = new Course();
            InitializeComponent();
                    
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await DatabaseService.GetTerm();
        }
        async void AddTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTerm());
        }

        public void MainExpander_Tapped(object sender, EventArgs e)
        {

        }

        async void CollectionView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.CurrentSelection != null)
            {
                Term term = (Term)e.CurrentSelection.FirstOrDefault();
                await Navigation.PushAsync(new EditTerm(term));
            }
        }
        
    }
}