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
using static WorkTogether.View.Users;

namespace WorkTogether.View
{
    /// <summary>
    /// Logique d'interaction pour Reservations.xaml
    /// </summary>
    public partial class Reservations : UserControl
    {
        public ObservableCollection<Order> allorder { get; set; }
        public Order? SelectedOrder { get; set; }
        public Reservations()
        {
            InitializeComponent();

            this.DataContext = this;

            using (WorktogetherContext allorders = new())
            {
                allorder = new ObservableCollection<Order>(allorders.Orders.ToList());
            }

            var columnsToDisplay = new Dictionary<string, string>
            {
                { "Id", "ID" },
                { "UserId", "ID Utilisateur" },
                { "RackId", "ID Rack" },
                { "BayId", "ID Baie" },
                { "Price", "Prix Mensuel" },
                { "Discount", "Réduction" },
                { "StartDate", "Date Départ" },
                { "EndDate", "Date Fin" }
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
                    case "Title":
                        dataColumn.Width = new DataGridLength(400);
                        break;
                    case "Description":
                        dataColumn.Width = new DataGridLength(400);
                        break;
                    default:
                        dataColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                        break;
                }
                dgOrder.Columns.Add(dataColumn);
            }

            dgOrder.AutoGenerateColumns = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddReservation addReservation = new AddReservation();
            addReservation.Show();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedOrder != null)
            {
                EditReservation editReservation = new EditReservation(SelectedOrder);
                editReservation.Show();
            }
        }
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.allorder != null && SelectedOrder != null)
            {
                using (WorktogetherContext context = new())
                {
                    context.Orders.Remove(SelectedOrder);
                    context.SaveChanges();
                    this.allorder.Remove(SelectedOrder);
                }
            }
        }
    }
}
