using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentTracker.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace StudentTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppSettings : ContentPage
    {
        public AppSettings()
        {
            InitializeComponent();
        }
        public static void ClearSettings_Clicked(object sender, EventArgs e)
        {
            Preferences.Clear();
        }
        public static void LoadSampleData_Clicked (object sender, EventArgs e)
        {
            DatabaseService.LoadSampleData();
        }
        public static void ClearSampleData_Clicked(object sender, EventArgs e)
        {
            DatabaseService.DeleteAllItems();
        }
    }
}