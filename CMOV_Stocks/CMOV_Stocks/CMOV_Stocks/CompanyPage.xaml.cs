using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CMOV_Stocks
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CompanyPage : ContentPage
	{
		public CompanyPage ()
		{
			InitializeComponent ();
		}

        async void NavigateBack(object sender, EventArgs e) {
            await Navigation.PopAsync();
        }
    }
}