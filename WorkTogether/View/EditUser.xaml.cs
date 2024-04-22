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

namespace WorkTogether.View;

/// <summary>
/// Logique d'interaction pour EditUser.xaml
/// </summary>
public partial class EditUser : Window
{
    public Dictionary<string, int> AccessLevels { get; set; } = new();
    private User _User;
    private int OldAccessLevel;

    public int SelectedAccessLevel
    {
        get { return OldAccessLevel; }
        set { OldAccessLevel = value; }
    }

    public User User
    {
        get { return _User; }
        set { _User = value; }
    }

    public EditUser(User user)
    {
        InitializeComponent();
        AccessLevels.Add("Client", 0);
        AccessLevels.Add("Comptable", 1);
        AccessLevels.Add("Administrateur", 2);
        this.User = user;
        DataContext = this;
        SelectedAccessLevel = user.Accesslevel;
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
        int accesslevel = ((KeyValuePair<string, int>)AccessLevelInput.SelectedItem).Value;

        if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(country) && int.TryParse(PostalCodeInput.Text, out postalCode))
        {
            User.Firstname = firstName;
            User.Lastname = lastName;
            User.Email = email;
            User.Address = address;
            User.Postalcode = postalCode;
            User.City = city;
            User.Country = country;
            User.Accesslevel = accesslevel;
            User.UpdatedAt = DateTime.Now;

            try
            {
                using (WorktogetherContext context = new WorktogetherContext())
                {
                    context.Users.Update(User);
                    context.SaveChanges();
                    this.Close();
                    MessageBox.Show("Utilisateur modifié avec succès.");
                }
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Une erreur est survenue lors de la modification de l'utilisateur : " + ex.Message);
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
