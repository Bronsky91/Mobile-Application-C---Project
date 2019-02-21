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
	public partial class CoursePage : ContentPage
	{
        
        private Course _currentCourse;
        private SQLiteAsyncConnection _connection;

        public CoursePage (Course course)
		{
            _currentCourse = course;

			InitializeComponent ();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            
        }

        protected override void OnAppearing()
        {

            Title = _currentCourse.CourseName;
            Status.Text = _currentCourse.Status;
            StartDate.Text = _currentCourse.StartDate.ToString("MM/dd/yyyy");
            EndDate.Text = _currentCourse.EndDate.ToString("MM/dd/yyyy");
            InstructorName.Text = _currentCourse.InstructorName;
            InstructorPhone.Text = _currentCourse.InstructorPhone;
            InstructorEmail.Text = _currentCourse.InstructorEmail;

            base.OnAppearing();
        }

        async void Assements_Button(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AssessmentsPage());
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EditCourse(_currentCourse));
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Warning", "Do you want to drop this course?", "Yes", "No");
            if (answer)
            {
                await _connection.DeleteAsync(_currentCourse);
                await Navigation.PopAsync();
            }
           
        }
    }
}