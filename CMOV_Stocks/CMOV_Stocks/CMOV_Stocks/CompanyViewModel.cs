using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace CMOV_Stocks
{
    class CompanyViewModel : ViewModelBase {
        Company item;

        public CompanyViewModel() {
            SaveOp = new Command(SaveItem);
        }

        public ObservableCollection<Company> CompanyList { set; get; }

        public Company Item {
            set { SetProperty(ref item, value); }
            get { return item; }
        }

        public INavigation Nav { get; set; }

        public ICommand SaveOp { get; set; }

        async void SaveItem() {
            if (Item.ID != 0) {
                int p = CompanyList.IndexOf(Item);
                CompanyList.Remove(Item);
                CompanyList.Insert(p, Item);
            } else
                CompanyList.Add(Item);
            await App.Database.SaveItemAsync(Item);
            await Nav.PopAsync();
        }
    }
}
