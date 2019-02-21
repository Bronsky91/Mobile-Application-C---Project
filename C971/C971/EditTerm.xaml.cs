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
	public partial class EditTerm : ContentPage
	{
        private SQLiteAsyncConnection _connection;
        private Term _currentTerm;

        public EditTerm (Term currentTerm)
		{
			InitializeComponent ();
            _currentTerm = currentTerm;
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<Term>();
            
            TermTitle.Text = _currentTerm.Title;
            startDate.Date = _currentTerm.StartDate;

            base.OnAppearing();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            _currentTerm.Title = TermTitle.Text;
            _currentTerm.StartDate = startDate.Date;
            _currentTerm.EndDate = endDate.Date;

            await _connection.UpdateAsync(_currentTerm);
            
            await Navigation.PopModalAsync();
        }
    }
}