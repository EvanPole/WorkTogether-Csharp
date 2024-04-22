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
using static WorkTogether.View.Customers;

namespace WorkTogether.View
{
    /// <summary>
    /// Logique d'interaction pour Users.xaml
    /// </summary>
    public partial class Users : UserControl
    {
        public ObservableCollection<User> alluser { get; set; }

        public User? SelectedUser { get; set; }

        public Users()
        {
            InitializeComponent();

            this.DataContext = this;

            using (WorktogetherContext allusers = new())
            {
                alluser = new ObservableCollection<User>(allusers.Users.ToList());
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
                { "Country", "Pays" },
                { "Accesslevel", "Habilitation" }
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
                    case "Accesslevel":
                        dataColumn.Width = new DataGridLength(75);
                        break;
                    default:
                        dataColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                        break;
                }
                dgUser.Columns.Add(dataColumn);
            }

            dgUser.AutoGenerateColumns = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.Show();
        }
     
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedUser != null)
            {
                EditUser editUser = new EditUser(SelectedUser);
                editUser.Show();
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
