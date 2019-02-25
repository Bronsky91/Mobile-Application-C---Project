using System;
using System.Collections;
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
	public partial class AddNewTerm : ContentPage
	{
        public MainPage _mainPage;
        private SQLiteAsyncConnection _connection;

        public AddNewTerm (MainPage mainPage)
		{
			InitializeComponent ();

            _mainPage = mainPage;
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }
        
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var newTerm = new Term();
            newTerm.Title = TermTitle.Text;
            newTerm.StartDate = startDate.Date;
            newTerm.EndDate = endDate.Date;

            if(newTerm.StartDate < newTerm.EndDate)
            {
                if (FieldValidation.nullCheck(newTerm.Title))
                {
                    await _connection.InsertAsync(newTerm);

                    _mainPage._termList.Add(newTerm);
                    await Navigation.PopModalAsync();
                }
                else
                    await DisplayAlert("Action Required", "Please make sure the Term Title is not blank", "Ok");
            }
            else
                await DisplayAlert("Action Required", "Please make sure the start date is before the end date", "Ok");


        }
    }
}