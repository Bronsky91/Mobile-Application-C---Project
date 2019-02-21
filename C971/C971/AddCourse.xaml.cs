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
	public partial class AddCourse : ContentPage
	{
        private SQLiteAsyncConnection _connection;

        public AddCourse (Term currentTerm)
		{
			InitializeComponent ();
            
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<Course>();

            base.OnAppearing();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var newCourse = new Course();
            newCourse.CourseName = CourseName.Text;
            newCourse.StartDate = startDate.Date;
            newCourse.EndDate = endDate.Date;
            newCourse.Status = CourseStatus.On ? "Enrolled" : "Not Enrolled";
            newCourse.InstructorName = InstructorName.Text;
            newCourse.InstructorPhone = InstructorPhone.Text;
            newCourse.InstructorEmail = InstructorEmail.Text;

            await _connection.InsertAsync(newCourse);
            
            await Navigation.PopModalAsync();
        }
    }
}