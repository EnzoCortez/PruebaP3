using PruebaP3.Views;

namespace PruebaP3
{
    public partial class App : Application
    {
        public App(SearchPage searchPage) //  Inyectar SearchPage
        {
            InitializeComponent();
            MainPage = new AppShell(searchPage); //  Pasar SearchPage a AppShell
        }
    }

}
