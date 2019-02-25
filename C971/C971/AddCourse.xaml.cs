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
        private Term _currentTerm;

        public AddCourse (Term currentTerm)
		{
			InitializeComponent ();
            _currentTerm = currentTerm;
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
            newCourse.Status = (string)CourseStatus.SelectedItem;
            newCourse.InstructorName = InstructorName.Text;
            newCourse.InstructorPhone = InstructorPhone.Text;
            newCourse.InstructorEmail = InstructorEmail.Text;
            newCourse.Notes = Notes.Text;
            newCourse.Term = _currentTerm.Id;


            if (FieldValidation.nullCheck(InstructorName.Text) &&
               FieldValidation.nullCheck(InstructorPhone.Text) &&
               FieldValidation.nullCheck(CourseName.Text))
            {
                if (FieldValidation.emailCheck(InstructorEmail.Text))
                {
                    if(newCourse.StartDate < newCourse.EndDate)
                    {
                        await _connection.InsertAsync(newCourse);

                        await Navigation.PopModalAsync();
                    }
                    else
                        await DisplayAlert("Action Required", "Please make sure the start date is before the end date", "Ok");
                }
                else
                    await DisplayAlert("Action Required", "Please make sure your email is valid before submitting", "Ok");
            } 
            else
                await DisplayAlert("Action Required", "Please make sure no fields are blank before submitting", "Ok");
            
        }
    }
}