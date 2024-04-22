using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkTogether.DB.Class;

namespace WorkTogether.View
{
    /// <summary>
    /// Logique d'interaction pour AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {

        public Dictionary<string, int> AccessLevels { get; set; } = new();
        public ObservableCollection<User> allUsers { get; set; }
        private PasswordHasher _Hasher { get; set; }
        public AddUser()
        {
            AccessLevels.Add("Client", 0);
            AccessLevels.Add("Comptable", 1);
            AccessLevels.Add("Administrateur", 2);
            InitializeComponent();
            DataContext = this;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            _Hasher = new();
            string firstName = FirstnameInput.Text;
            string lastName = LastnameInput.Text;
            string email = EmailInput.Text;
            int postalCode;
            string address = AdressInput.Text;
            string city = CityInput.Text;
            string country = CountryInput.Text;
            int accessLevel = ((KeyValuePair<string,int>)AccessLevelInput.SelectedItem).Value;
            string password = _Hasher.HashPassword(PasswordInput.Text);

            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(country) && !string.IsNullOrEmpty(password) && int.TryParse(PostalCodeInput.Text, out postalCode))
            {
                User newUser = new User
                {
                    Lastname = lastName,
                    Firstname = firstName,
                    Email = email,
                    Address = address,
                    Postalcode = postalCode,
                    City = city,
                    Country = country,
                    Accesslevel = accessLevel,
                    Password = password,
                    EmailVerifiedAt = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                try
                {
                    using (WorktogetherContext context = new WorktogetherContext())
                    {
                        context.Users.Add(newUser);
                        context.SaveChanges();
                        this.Close();
                        MessageBox.Show("Utilisateur ajouté avec succès.");
                    }
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show("Une erreur est survenue lors de l'ajout de l'utilisateur : " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Une erreur est survenue : " + ex.Message);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs");
            }
        }

    }
}  
