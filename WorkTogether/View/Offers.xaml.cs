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
    /// Logique d'interaction pour Offers.xaml
    /// </summary>
    public partial class Offers : UserControl
    {
        public ObservableCollection<Offer> alloffer { get; set; }
        public Offer? SelectedOffer { get; set; }
        public Offers()
        {
            InitializeComponent();

            this.DataContext = this;

            using (WorktogetherContext alloffers = new())
            {
                alloffer = new ObservableCollection<Offer>(alloffers.Offers.ToList());
            }

            var columnsToDisplay = new Dictionary<string, string>
            {
                { "Id", "ID" },
                { "Title", "Titre" },
                { "Description", "Description" },
                { "RackQty", "Quantité Rack" },
                { "Price", "Prix mensuel" },
                { "Discount", "Réduction" }
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
                dgOffer.Columns.Add(dataColumn);
            }

            dgOffer.AutoGenerateColumns = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddOffer addOffer = new AddOffer();
            addOffer.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedOffer != null)
            {
                EditOffer editOffer = new EditOffer(SelectedOffer);
                editOffer.Show();   
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.alloffer != null && SelectedOffer != null)
            {
                using (WorktogetherContext context = new())
                {
                    context.Offers.Remove(SelectedOffer);
                    context.SaveChanges();
                    this.alloffer.Remove(SelectedOffer);
                }
            }
        }
    }
}
