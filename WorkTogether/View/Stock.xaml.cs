using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    /// Logique d'interaction pour Stock.xaml
    /// </summary>
    public partial class Stock : UserControl
    {
        public ObservableCollection<Bay> allbay { get; set; }

        public Bay? SelectedBay { get; set; }

        public Stock()
        {
            InitializeComponent();

            this.DataContext = this;

            using (WorktogetherContext allbays = new())
            {
                allbay = new ObservableCollection<Bay>(allbays.Bays.ToList());
            }

            // Définition des colonnes existantes
            var columnsToDisplay = new Dictionary<string, string>
            {
                { "Id", "ID" },
                { "BayName", "Nom" }
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
                    default:
                        dataColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                        break;
                }
                dgStock.Columns.Add(dataColumn);
            }

            dgStock.AutoGenerateColumns = false;
        }



        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddStock addStock = new AddStock();
            addStock.Show();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditStock editStock = new EditStock(SelectedBay);
            editStock.Show();
        }
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.allbay != null && SelectedBay != null)
            {
                using (WorktogetherContext context = new())
                {
                    context.Bays.Remove(SelectedBay);
                    context.SaveChanges();
                    this.allbay.Remove(SelectedBay);
                }
            }
        }
    }
}
