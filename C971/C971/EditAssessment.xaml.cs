using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditAssessment : ContentPage
	{
        private SQLiteAsyncConnection _connection;
        private Assessment _assessment;

		public EditAssessment (Assessment assessment)
		{
			InitializeComponent ();

            _assessment = assessment;
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<Assessment>();

            AssessmentTitle.Text = _assessment.Title;
            StartDate.Date = _assessment.StartDate;
            EndDate.Date = _assessment.EndDate;
            EnableNotifications.On = _assessment.NotificationEnabled == 1 ? true : false;
            
            base.OnAppearing();
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            _assessment.Title = AssessmentTitle.Text;
            _assessment.StartDate = StartDate.Date;
            _assessment.EndDate = EndDate.Date;
            _assessment.NotificationEnabled = EnableNotifications.On == true ? 1 :0;

            await _connection.UpdateAsync(_assessment);

            await Navigation.PopModalAsync();
               
        }
    }
}