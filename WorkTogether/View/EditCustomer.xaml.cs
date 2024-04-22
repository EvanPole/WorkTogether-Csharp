using Microsoft.EntityFrameworkCore;
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

namespace WorkTogether.View
{
    /// <summary>
    /// Logique d'interaction pour EditCustomer.xaml
    /// </summary>
    public partial class EditCustomer : Window
    {
        private User _User;
        public User User
        {
            get { return _User; }
            set { _User = value; }
        }

        private User userToUpdate;
        public EditCustomer(User user)
        {
            userToUpdate = user;
            if (userToUpdate == null)
            {
                MessageBox.Show("Veuillez sélectionner un client à modifier");
                this.Close();
            }
            else
            {
                InitializeComponent();
                this.User = userToUpdate;
                DataContext = this;
            }
        }
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstnameInput.Text;
            string lastName = LastnameInput.Text;
            string email = EmailInput.Text;
            string address = AddressInput.Text;
            int postalCode;
            string city = CityInput.Text;
            string country = CountryInput.Text;
            int accesslevel = 0;

            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(country) && int.TryParse(PostalCodeInput.Text, out postalCode))
            {
                userToUpdate.Firstname = firstName;
                userToUpdate.Lastname = lastName;
                userToUpdate.Email = email;
                userToUpdate.Address = address;
                userToUpdate.Postalcode = postalCode;
                userToUpdate.City = city;
                userToUpdate.Country = country;
                userToUpdate.Accesslevel = accesslevel;
                userToUpdate.UpdatedAt = DateTime.Now;

                try
                {
                    using (WorktogetherContext context = new WorktogetherContext())
                    {
                        context.Users.Update(userToUpdate);
                        context.SaveChanges();
                        this.Close();
                        MessageBox.Show("Client modifié avec succès.");
                    }
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show("Une erreur est survenue lors de la modification du client : " + ex.Message);
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
