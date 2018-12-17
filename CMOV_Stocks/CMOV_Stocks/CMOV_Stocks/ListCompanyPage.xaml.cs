using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CMOV_Stocks
{
    public partial class ListCompanyPage : ContentPage
    {
        int ButtonCount = 0;
        Company lastItemPressed;
        Stack stopTokens = new Stack();

        Company doubleClickSelected = null;

        public ListCompanyPage()
        {
            Title = "My Stocks";
            InitializeComponent();
            BindingContext = new CompanyListViewModel();
        }

        public void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            //for first press
            if (lastItemPressed == null) {
                lastItemPressed = (Company) e.Item;
            }


            if (((Company) e.Item).Equals(lastItemPressed)) {
                if (ButtonCount < 1) {
                    Device.StartTimer(TimeSpan.FromMilliseconds(500), TapHandler);
                }
            } else {
                if (ButtonCount > 0) {
                    ButtonCount = 0;
                    stopTokens.Push("Token");
                }

                Device.StartTimer(TimeSpan.FromMilliseconds(500), TapHandler);
            }

            Console.WriteLine("Last Pressed = " + lastItemPressed);
            Console.WriteLine("Current Press = " + e.Item.ToString());
            Console.WriteLine("Count = " + ButtonCount);
            lastItemPressed = (Company) e.Item;
            ButtonCount++;
            ((ListView) sender).SelectedItem = null;
        }

        bool TapHandler() {
            if (stopTokens.Count == 0) {
                if (ButtonCount > 1) {
                    //double click
                    DisplayAlert("", "Two Clicks - " + lastItemPressed.ToString(), "OK");

                    if (doubleClickSelected != null && doubleClickSelected.Equals(lastItemPressed)) {
                        doubleClickSelected = null;
                        lastItemPressed.Selected = false;
                    } else {
                        doubleClickSelected = lastItemPressed;
                        lastItemPressed.Selected = true;
                    }
                    
                } else {
                    //single click
                    DisplayAlert("", "One Click - " + lastItemPressed.ToString(), "OK");
                    if (doubleClickSelected != null) {
                        //send to comparison interface
                    } else {
                        SkiaPage skiaPage = new SkiaPage();
                        skiaPage.Company1 = lastItemPressed;
                        Navigation.PushAsync(new NavigationPage(new SkiaPage()));
                        //send to single company interface
                    }
                }

                ButtonCount = 0;
            } else {
                stopTokens.Pop();
            }

            return false;
        }
    }
}
