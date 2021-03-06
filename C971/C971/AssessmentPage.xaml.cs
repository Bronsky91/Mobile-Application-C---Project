﻿using System;
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
	public partial class AssessmentPage : ContentPage
    {
        private Assessment _assessment;

        private SQLiteAsyncConnection _connection;

        public AssessmentPage (Assessment assessment)
		{
			InitializeComponent ();
            _assessment = assessment;

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override void OnAppearing()
        {
            Title = _assessment.Type;
            AssessmentName.Text = _assessment.Title;
            StartDate.Text = _assessment.StartDate.ToString("MM/dd/yyyy");
            EndDate.Text = _assessment.EndDate.ToString("MM/dd/yyyy");
            AssessmentNotification.Text = _assessment.NotificationEnabled == 1 ? "Yes" : "No";

            base.OnAppearing();
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EditAssessment(_assessment));
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Warning", "Do you want to delete this Assessment?", "Yes", "No");
            if (answer)
            {
                await _connection.DeleteAsync(_assessment);
                await Navigation.PopAsync();
            }

        }
    }
}