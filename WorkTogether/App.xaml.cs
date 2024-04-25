using System.Configuration;
using System.Data;
using System.Windows;
using WorkTogether.DB.Class;
using WorkTogether.Windows;

namespace WorkTogether
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public User? LoggedUser { get; set; }

        public void Login(User user)
        {
            LoggedUser = user;
            MainWindow mainWindow = new();
            App.Current.MainWindow.Close();
            App.Current.MainWindow = mainWindow;
            Task.Delay(100);
            mainWindow.Show();
        }
        public void Logout()
        {
            LoggedUser = null;
            WindowLogin windowLogin = new();
            App.Current.MainWindow.Close();
            App.Current.MainWindow = windowLogin;
            windowLogin.Show();
        }
    }

}
