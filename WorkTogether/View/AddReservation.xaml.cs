using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
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
    /// Logique d'interaction pour AddReservations.xaml
    /// </summary>
    public partial class AddReservation : Window
    {
        public ObservableCollection<Order> allOrders { get; set; }
        public ObservableCollection<User> UserID { get; set; }
        public ObservableCollection<Bay> BayID { get; set; }
        public ObservableCollection<Rack> RackID { get; set; }
        public AddReservation()
        {
            InitializeComponent();
            DataContext = this;
            UserID = GetUserID();
            BayID = GetBayID();
            BayIDComboBox.SelectionChanged += ComboBoxBays_SelectionChanged;
        }

        private ObservableCollection<User> GetUserID()
        {
            using (WorktogetherContext context = new WorktogetherContext())
            {
                return new ObservableCollection<User>(context.Users.ToList());
            }
        }

        private ObservableCollection<Bay> GetBayID()
        {
            using (WorktogetherContext context = new WorktogetherContext())
            {
                return new ObservableCollection<Bay>(context.Bays.ToList());
            }
        }

        private void ComboBoxBays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BayIDComboBox.SelectedItem is Bay selectedBay)
            {
                GetRackForSelectedBay(selectedBay);
                RackIDComboBox.ItemsSource = RackID;
            }
        }

        private void GetRackForSelectedBay(Bay selectedBay)
        {
            using (WorktogetherContext context = new WorktogetherContext())
            {
                RackID = new ObservableCollection<Rack>(
                    context.Racks
                           .Where(sub => sub.BayId == selectedBay.Id)
                           .ToList());

                RackIDComboBox.ItemsSource = RackID;
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (
                UserIDComboBox.SelectedItem is User selectedUserItem &&
                BayIDComboBox.SelectedItem is Bay selectedBayItem &&
                RackIDComboBox.SelectedItem is Rack selectedRackItem &&
                int.TryParse(PriceInput.Text, out int price) &&
                    int.TryParse(DiscountInput.Text, out int discount) &&
                    StartDateInput.SelectedDate.HasValue &&
                    EndDateInput.SelectedDate.HasValue)
            {
                if (StartDateInput.SelectedDate.Value <= EndDateInput.SelectedDate.Value)
                {
                    ulong userId = (ulong)selectedUserItem.Id;
                    ulong bayId = (ulong)selectedBayItem.Id;
                    ulong rackId = (ulong)selectedRackItem.Id;

                    Order newOrder = new Order
                    {
                        UserId = userId,
                        BayId = bayId,
                        RackId = rackId,
                        Price = price,
                        Discount = discount,
                        StartDate = StartDateInput.SelectedDate.Value,
                        EndDate = EndDateInput.SelectedDate.Value,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                    };

                    try
                    {
                        using (WorktogetherContext context = new WorktogetherContext())
                        {
                            context.Orders.Add(newOrder);
                            context.SaveChanges();
                            this.Close();
                            MessageBox.Show("Réservations ajoutée avec succès");
                        }
                    }
                    catch (DbUpdateException ex)
                    {
                        MessageBox.Show("Une erreur est survenue lors de l'ajout de la réservation : " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Une erreur est survenue : " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("La date de départ est supérieur à la date de fin");
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs");
            }
        }
    }
}
