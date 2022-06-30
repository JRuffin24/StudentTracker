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
            InitializeComponent();
            
        }

        async void AddTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClassDetails());
        }

       async void EditTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditTerm());
        }

        public void MainExpander_Tapped(object sender, EventArgs e)
        {

        }
    }
}