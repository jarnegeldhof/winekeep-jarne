using Prb.Wine.Keeper.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

namespace Pra.Wine.Keeper.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
         public WineCollection winecollection = new WineCollection();
         

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Seedwinecollection();
            Populatewineboxes(winecollection);
            grpWineDetails.IsEnabled = true;
            dtpPurchaseDate.SelectedDate = DateTime.Now;
        }
        private void lstWine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDrinkBottle.IsEnabled = true;
            WineBox Selectedwinebox = (WineBox)lstWine.SelectedItem;
            GetWineboxData(Selectedwinebox);
           

        }
        private void BtnDrinkBottle_Click(object sender, RoutedEventArgs e)
        {
            WineBox selectedWinebox = (WineBox)lstWine.SelectedItem;
            lstWine.Items.Clear();
            DrinkABottle(selectedWinebox);
            Populatewineboxes(winecollection);
            lstWine.SelectedItem = selectedWinebox;
            

        }

        private void btnAddWine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddWinebox(txtDescription.Text, txtMinStorage.Text, txtMaxStorage.Text, dtpPurchaseDate.SelectedDate, txtNumBottles.Text);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            lstWine.Items.Clear ();
            Populatewineboxes(winecollection);

        }
        private void Populatewineboxes(WineCollection wineCollection)
        {
           
            foreach (WineBox box in winecollection.wineBoxes)
            {
                lstWine.Items.Add(box);
            }
        }

        private void DrinkABottle(WineBox winebox)
        {
            if (winebox.NumberOfBottles > 1)
            {
            winebox.NumberOfBottles--;
            }
            else
            {
                MessageBoxResult result = MessageBox.Show($"Dit is de laatste wijn in de box {winebox} \n weet je zeker dat je deze wilt drinken", "Laatste fles", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    winecollection.RemoveWineBox(winebox);
                }
            }

        }
        private void TxtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstWine.Items.Clear();
            
            
            if (txtFilter.Text != null)
            {
                
                WineCollection filteredcollection = new WineCollection();
                List<WineBox> filterdlist = winecollection.FilteringWineBoxes(winecollection.wineBoxes, txtFilter.Text, dtpFilter.SelectedDate);
                foreach (WineBox box in filterdlist)
                {
                    filteredcollection.AddWineBox(box);
                }
                Populatewineboxes(filteredcollection);
            }
            else
            {
                
                Populatewineboxes(winecollection);

            }
           
        }
        private void dtpFilter_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            winecollection.FilteringWineBoxes(winecollection.wineBoxes, txtFilter.Text, dtpFilter.SelectedDate);

        }
         
           
        
       private void Seedwinecollection()
        {
            
            List<WineBox> wineBoxes = WineSeeder.GetWineBoxes();

            foreach (WineBox box in wineBoxes)
            {
                winecollection.AddWineBox(box);
            }
        }
        private void AddWinebox (string description, string minStorage, string maxStorage, DateTime? purchaseDate, string numBottles)
        {

            ValidateNewWineInputs(description, minStorage, maxStorage, purchaseDate, numBottles);


            int minStorageMonths = int.Parse(minStorage);
            int maxStorageMonths = int.Parse(maxStorage);
            int bottles = int.Parse(numBottles);


            WineBox newWineBox = new WineBox(description, minStorageMonths, maxStorageMonths, purchaseDate.Value,bottles);

           
            winecollection.AddWineBox(newWineBox);

           
            txtDescription.Clear();
            txtMinStorage.Clear();
            txtMaxStorage.Clear();
            dtpPurchaseDate.SelectedDate = DateTime.Now;
            txtNumBottles.Clear();
        }
        private void ValidateNewWineInputs(string description, string minStorageText, string maxStorageText, DateTime? purchaseDate, string numBottlesText)
        {
            
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new FormatException("Description mag niet leeg zijn");
            }

            
            if (!int.TryParse(minStorageText, out int result )|| result < 0)
            {
                throw new FormatException("MinStorage moet een positief getal zijn.");
            }

            
            if (!int.TryParse(maxStorageText, out int maxresult) || maxresult < 0)
            {
                throw new FormatException("MaxStorage moet een positief getal zijn.");
            }

            
            if (!purchaseDate.HasValue || purchaseDate.Value > DateTime.Now)
            {
                throw new FormatException("De datum moet vandaag zijn of in het verleden liggen.");
            }

            
            if (!int.TryParse(numBottlesText, out int numBottles) || numBottles < 0)
            {
                throw new FormatException("Aantal flessen moet een positief getal zijn.");
            }
        }

        private void GetWineboxData(WineBox wineBox)
        {
            if (wineBox != null)
            {
                txtDescription.Text = wineBox.Description;
                txtMinStorage.Text = wineBox.MinStorageMonths.ToString();
                txtMaxStorage.Text = wineBox.MaxStorageMonths.ToString();
                dtpPurchaseDate.SelectedDate = wineBox.PurchaseDate;
                txtNumBottles.Text = wineBox.NumberOfBottles.ToString();
            }
        }
       

    }
}
