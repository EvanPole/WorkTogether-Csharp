
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkTogether.DB.Class;

namespace WorkTogether.View
{
    /// <summary>
    /// Logique d'interaction pour Customers.xaml
    /// </summary>
    public partial class Customers : UserControl
    {
        public ObservableCollection<User> alluser { get; set; }
        public User? SelectedUser { get; set; }
        public Customers()
        {
            InitializeComponent();

            this.DataContext = this;

            using (WorktogetherContext allusers = new())
            {
                // Filtrer les utilisateurs avec un accesslevel de 0
                alluser = new ObservableCollection<User>(allusers.Users.Where(u => u.Accesslevel == 0).ToList());
            }

            var columnsToDisplay = new Dictionary<string, string>
            {
                { "Id", "ID" },
                { "Firstname", "Prénom" },
                { "Lastname", "Nom" },
                { "Email", "Email" },
                { "Address", "Adresse" },
                { "Postalcode", "Code Postal" },
                { "City", "Ville" },
            };

            foreach (var column in columnsToDisplay)
            {
                DataGridTextColumn dataColumn = new DataGridTextColumn();
                dataColumn.Header = column.Value;
                dataColumn.Binding = new Binding(column.Key);
                switch (column.Key)
                {
                    case "Id":
                        dataColumn.Width = new DataGridLength(30);
                        break;
                    case "Firstname":
                        dataColumn.Width = new DataGridLength(150);
                        break;
                    case "Lastname":
                        dataColumn.Width = new DataGridLength(150);
                        break;
                    case "Postalcode":
                        dataColumn.Width = new DataGridLength(75);
                        break;
                    default:
                        dataColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                        break;
                }
                dgCustomer.Columns.Add(dataColumn);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddCustomer addCustomer = new AddCustomer();
            addCustomer.Show();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedUser != null)
            {
                EditCustomer editCustomer = new EditCustomer(SelectedUser);
                editCustomer.Show();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un utilisateur à modifier !");
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.alluser != null && SelectedUser != null)
            {
                using (WorktogetherContext context = new())
                {
                    context.Users.Remove(SelectedUser);
                    context.SaveChanges();
                    this.alluser.Remove(SelectedUser);
                }
            }
        }
    }
}
