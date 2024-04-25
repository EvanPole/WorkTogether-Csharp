using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorkTogether.DB.Class;

namespace WorkTogether.Windows
{
    /// <summary>
    /// Logique d'interaction pour WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        public WindowLogin()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailInput.Text;
            string password = PasswordInput.Password;

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                User? user = null;
                bool result = false;
                using (WorktogetherContext context = new())
                {
                    IEnumerable<User> users = context.Users.ToList();
                    user = context.Users.FirstOrDefault(userTemp => userTemp.Email.Equals(email));
                }

                if (user is not null)
                {
                    result = BCrypt.Net.BCrypt.Verify(password, user.Password);
                    if (result)
                    {
                        Console.WriteLine(result.ToString());
                        ((App)App.Current).Login(user);
                    }
                }

/*                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();*/
            }
            else
            {
                MessageBox.Show("Veuillez saisir une adresse e-mail et un mot de passe");
            }
        }
    }
}