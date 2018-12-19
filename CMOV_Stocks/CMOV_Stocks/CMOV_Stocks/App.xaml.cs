using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CMOV_Stocks {
    public partial class App : Application {

        static CompanyDatabase database;
        public App() {
            InitializeComponent();

            //MainPage = new NavigationPage(new SkiaPage());
            MainPage = new NavigationPage(new ListCompanyPage());
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }

        public static CompanyDatabase Database {
            get {
                if (database == null) {
                    string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),"database.db3");
                    //string dbPath = DependencyService.Get<IFileHelper>().GetLocalFilePath("database.db3");
                    database = new CompanyDatabase(dbPath);
                }
                return database;
            }
        }
    }
}
