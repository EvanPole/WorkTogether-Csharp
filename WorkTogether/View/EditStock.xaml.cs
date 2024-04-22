using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
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
    /// Logique d'interaction pour EditStock.xaml
    /// </summary>
    public partial class EditStock : Window
    {
        private Bay _Bay;

        public Bay Bay
        {
            get { return _Bay; }
            set { _Bay = value; }
        }

        public EditStock(Bay bay)
        {
            InitializeComponent();
            this.Bay = bay;
            DataContext = this;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            string bayName = NameInput.Text;

            if (!string.IsNullOrEmpty(bayName))
            {
                Bay.BayName = bayName;
                Bay.UpdatedAt = DateTime.Now;

                try
                {
                    using (WorktogetherContext context = new WorktogetherContext())
                    {
                        context.Bays.Update(Bay);
                        context.SaveChanges();
                        this.Close();
                        MessageBox.Show("Baie modifiée avec succès.");
                    }
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show("Une erreur est survenue lors de la modification de la baie : " + ex.Message);
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
