using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;

namespace C971
{
    [Table("Terms")]
    public class Term
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    [Table("Courses")]
    public class Course
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string TermTitle { get; set; }
        public string CourseName { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string InstructorName { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }
        public string Notes { get; set; }
    }

    public partial class MainPage : ContentPage
    {

        private SQLiteAsyncConnection _connection;
        public ObservableCollection<Term> _termList;
        
        
        public MainPage()
        {
            InitializeComponent();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            termListView.ItemTapped += new EventHandler<ItemTappedEventArgs>(Term_Clicked);
        }

        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<Term>();

            var termList = await _connection.Table<Term>().ToListAsync();
            _termList = new ObservableCollection<Term>(termList);
            termListView.ItemsSource = _termList;

            base.OnAppearing();
        }

        async private void Term_Clicked(object sender, ItemTappedEventArgs e)
        {
            Term term = (Term)e.Item;
            await Navigation.PushAsync(new TermPage(term));
        }

        private async void Add_Term(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddNewTerm(this));
        }

        private async void Delete_Term(object sender, EventArgs e)
        {
            var lastTerm = _termList.Last();

            await _connection.DeleteAsync(lastTerm);

            _termList.Remove(lastTerm);
        }

    }
}
