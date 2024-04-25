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
using static WorkTogether.MainWindow;

namespace WorkTogether.View
{
    /// <summary>
    /// Logique d'interaction pour AddOffer.xaml
    /// </summary>
    public partial class AddOffer : Window
    {

        public ObservableCollection<Offer> allOffers { get; set; }
        public Offer SelectedOffer { get; set; }
        public AddOffer()
        {
            PageManager.CurPage = true;
            InitializeComponent();
            DataContext = this;
            DiscountInput.Text = "0";
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleInput.Text;
            string description = DescriptionInput.Text;
            string erreur = "";
            int rackQty;
            int price;
            int discount;

            if (string.IsNullOrEmpty(title))
            {
                erreur = erreur + " - " + "Merci de remplire correctement le Titre \n";
                TitleInput.Background = Brushes.Red;

            }else
            {
                TitleInput.Background = Brushes.White;

            }
            if (string.IsNullOrEmpty(description))
            {
                erreur = erreur + " - " + "Merci de remplire correctement la description \n";
                DescriptionInput.Background = Brushes.Red;


            }
            else
            {
                DescriptionInput.Background = Brushes.White;

            }
            if (!int.TryParse(RackQtyInput.Text, out rackQty))
            {
                erreur = erreur + " - " + "Merci de remplire correctement la quantité \n";
                RackQtyInput.Background = Brushes.Red;


            }
            else
            {
                RackQtyInput.Background = Brushes.White;

            }
            if (!int.TryParse(PriceInput.Text, out price))
            {
                erreur = erreur + " - " + "Merci de remplire correctement le prix \n";
                PriceInput.Background = Brushes.Red;


            }
            else
            {
                PriceInput.Background = Brushes.White;

            }
            if (!int.TryParse(DiscountInput.Text, out discount))
            {
                erreur = erreur + " - " + "Merci de remplire correctement la réduction \n";
                DiscountInput.Background = Brushes.Red;


            }
            else
            {
                DiscountInput.Background = Brushes.White;

            }

            if (!string.IsNullOrEmpty(title) 
                && !string.IsNullOrEmpty(description) 
                && int.TryParse(RackQtyInput.Text, out rackQty) 
                && int.TryParse(PriceInput.Text, out price)
                && int.TryParse(DiscountInput.Text, out discount))
            {

                Offer newOffer = new Offer
                {
                    Title = title,
                    Description = description,
                    RackQty = rackQty,
                    Price = price,
                    Discount = discount,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                try
                {
                    using (WorktogetherContext context = new WorktogetherContext())
                    {                                                                                                                   
                        context.Offers.Add(newOffer);
                        context.SaveChanges();
                        this.Close();
                        MessageBox.Show("Offre ajoutée avec succès.");
                        PageManager.CurPage = false;

 


                    }
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show("Une erreur est survenue lors de la modification de l'offre : " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Une erreur est survenue : " + ex.Message);
                }
                this.Close();
            } 
            else
            {
                MessageBox.Show(erreur);
            }
        }
    }
}
