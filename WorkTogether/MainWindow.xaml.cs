using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkTogether.View;

namespace WorkTogether
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new Home());
        }
        private void ButtonUsers_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new Users());
        }
        private void ButtonStock_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new Stock());
        }
        private void ButtonOffers_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new Offers());
        }
        private void ButtonCustomers_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new Customers());
        }
        private void ButtonReservations_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new Reservations());
        }
    }
}