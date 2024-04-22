using Microsoft.EntityFrameworkCore;
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
using System.Windows.Shapes;
using WorkTogether.DB.Class;

namespace WorkTogether.View
{
    /// <summary>
    /// Logique d'interaction pour AddStock.xaml
    /// </summary>
    public partial class AddStock : Window
    {
        public ObservableCollection<Bay> allbays { get; set; }
        public Offer SelectedBay { get; set; }
        public AddStock()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameInput.Text;

            if (!string.IsNullOrEmpty(name) && int.TryParse(name, out int bayNumber))
            {
                // Formater le numéro de baie avec trois chiffres et le préfixe "B-"
                string formattedBayNumber = bayNumber.ToString().PadLeft(3, '0');
                string bayName = "B" + formattedBayNumber;

                // Vérifier si le numéro de baie est déjà pris dans la base de données
                using (WorktogetherContext context = new WorktogetherContext())
                {
                    if (context.Bays.Any(b => b.BayName == bayName))
                    {
                        MessageBox.Show("Ce numéro de baie est déjà pris. Veuillez choisir un autre numéro");
                        return;
                    }
                }

                Bay newBay = new Bay
                {
                    BayName = bayName,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                try
                {
                    using (WorktogetherContext context = new WorktogetherContext())
                    {
                        context.Bays.Add(newBay);
                        context.SaveChanges();
                        this.Close();
                        MessageBox.Show("Baie ajoutée avec succès");
                    }
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show("Une erreur est survenue lors de l'ajout de la baie : " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Une erreur est survenue : " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer un numéro de baie valide");
            }
        }
    }
}
