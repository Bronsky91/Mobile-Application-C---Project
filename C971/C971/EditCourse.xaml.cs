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
	public partial class EditCourse : ContentPage
	{
        private SQLiteAsyncConnection _connection;
        private Course _currentCourse;

        public EditCourse (Course currentCourse)
		{
			InitializeComponent ();

            _currentCourse = currentCourse;
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<Course>();

            CourseName.Text = _currentCourse.CourseName;
            CourseStatus.On = _currentCourse.Status == "Enrolled" ? true : false;
            StartDate.Date = _currentCourse.StartDate;
            EndDate.Date = _currentCourse.EndDate;
            InstructorName.Text = _currentCourse.InstructorName;
            InstructorPhone.Text = _currentCourse.InstructorPhone;
            InstructorEmail.Text = _currentCourse.InstructorEmail;

            base.OnAppearing();
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            _currentCourse.CourseName = CourseName.Text;
            _currentCourse.StartDate = StartDate.Date;
            _currentCourse.EndDate = EndDate.Date;
            _currentCourse.Status = CourseStatus.On ? "Enrolled" : "Not Enrolled";
            _currentCourse.InstructorName = InstructorName.Text;
            _currentCourse.InstructorPhone = InstructorPhone.Text;
            _currentCourse.InstructorEmail = InstructorEmail.Text;

            await _connection.UpdateAsync(_currentCourse);

            await Navigation.PopModalAsync();
        }
    }
}