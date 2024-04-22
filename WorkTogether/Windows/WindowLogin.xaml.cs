using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNet.Identity;
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

namespace WorkTogether.Windows
{
    /// <summary>
    /// Logique d'interaction pour WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        private PasswordHasher _Hasher { get; set; }
        public WindowLogin()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            _Hasher = new();
            string email = EmailInput.Text;
            string password = _Hasher.HashPassword(PasswordInput.Password);

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez saisir une adresse e-mail et un mot de passe");
            }
        }
    }
}
