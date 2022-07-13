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
    public partial class AssessmentDetails : ContentPage
    {
        public AssessmentDetails()
        {
            InitializeComponent();
        }
        private void Notifications_OnToggle(object sender, EventArgs e)
        {

        }
        async void EditAssessmentButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditAssessment());
        }
        private void CancelButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}