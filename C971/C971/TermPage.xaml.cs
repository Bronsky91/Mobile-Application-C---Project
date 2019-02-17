using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TermPage : ContentPage
	{
		public TermPage (string termNumber)
		{
			InitializeComponent ();
            Title = termNumber;
		}

        async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CoursePage());
        }
    }
}