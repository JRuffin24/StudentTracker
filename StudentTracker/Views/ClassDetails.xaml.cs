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
    public partial class ClassDetails : ContentPage
    {
        public ClassDetails()
        {
            InitializeComponent();
        }
        async void AssessmentButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Assessments());
        }
        async void EditClassButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditClass());
        }
        private void Notifications_OnToggle(object sender, EventArgs e)
        {

        }
    }
}