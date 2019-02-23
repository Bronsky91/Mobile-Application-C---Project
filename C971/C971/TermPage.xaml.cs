using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TermPage : ContentPage
	{
        
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Course> _courseList;
        private Term _currentTerm;
        

        public TermPage (Term term)
		{
			InitializeComponent ();
            Title = term.Title;
            _currentTerm = term;
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            courseListView.ItemTapped += new EventHandler<ItemTappedEventArgs>(Course_Tapped);
        }

        protected override async void OnAppearing()
        {
            TermDetailsStart.Text = $"Term Start: {_currentTerm.StartDate.ToString("MM/dd/yyyy")}";
            TermDetailsEnd.Text = $"Term End: { _currentTerm.EndDate.ToString("MM/dd/yyyy")}";

            await _connection.CreateTableAsync<Course>();
            // var courseList = await _connection.Table<Course>().ToListAsync();
            var courseList = await _connection.QueryAsync<Course>($"SELECT * FROM Courses WHERE Term = '{_currentTerm.Id}'");
            _courseList = new ObservableCollection<Course>(courseList);
            courseListView.ItemsSource = _courseList;
            
            base.OnAppearing();
        }
        
        private async void Course_Tapped(object sender, ItemTappedEventArgs e)
        {
            Course course = (Course)e.Item;
            await Navigation.PushAsync(new CoursePage(course));
        }

        async void Edit_Term(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EditTerm(_currentTerm));
        }

        private async void Add_Course(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new AddCourse(_currentTerm));
            //newCourse.TermTitle = _currentTerm.Title;

        }
        
    }
}