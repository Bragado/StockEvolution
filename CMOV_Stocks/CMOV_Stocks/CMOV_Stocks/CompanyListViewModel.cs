using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CMOV_Stocks
{
    class CompanyListViewModel : ViewModelBase {
        ObservableCollection<Company> companyList = null;

        public ObservableCollection<Company> Companies {
            set { SetProperty(ref companyList, value); }
            get {
                if (companyList == null)
                    Initialize();
                return companyList;
            }
        }

        private void Initialize() {
            List<Company> list = new List<Company>();
            list.Add(new Company("APPL", "Apple"));
            list.Add(new Company("IBM", "IBM"));
            list.Add(new Company("HPM", "Hewlett Packard"));
            list.Add(new Company("MSFT", "Microsoft"));
            list.Add(new Company("ORCL", "Oracle"));
            list.Add(new Company("GOOGL", "Google"));
            list.Add(new Company("FB", "Facebook"));
            list.Add(new Company("TWTR", "Twitter"));
            list.Add(new Company("INTC", "Intel"));
            list.Add(new Company("AMD", "AMD"));

            Companies = new ObservableCollection<Company>(list);
        }
    }
}
