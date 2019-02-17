using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace C971
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            int numberOfTerms = 4;

            for(int i=1; i <= numberOfTerms; i++)
            {
                var term = new Button {Text = "Term " + i.ToString(), HorizontalOptions=LayoutOptions.Center, Margin=new Thickness(0, 20, 0, 20)};
                term.Clicked += Term_Clicked;
                terms.Children.Add(term);
            }

        }

        async private void Term_Clicked(object sender, EventArgs e)
        {
            string buttonText = ((Button)sender).Text;
            await Navigation.PushAsync(new TermPage(buttonText));
        }
        
    }
}
