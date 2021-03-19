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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Prb.Compositie.Voorbeeld1.Core.Entities;
using Prb.Compositie.Voorbeeld1.Core.Services;

namespace Prb.Compositie.Voorbeeld1.Wpf
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

        GlobalService globalService;
        bool isNew;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            globalService = new GlobalService();
            PopulateComboboxes();
            DefaultSituation();
            PopulateListbox();
            ClearControls();            
        }
        private void PopulateComboboxes()
        {
            cmbGroups.ItemsSource = null;
            cmbVATs.ItemsSource = null;

            cmbGroups.ItemsSource = globalService.Groups;
            cmbVATs.ItemsSource = globalService.VATS;
        }
        private void DefaultSituation()
        {
            grpProducts.IsEnabled = true;
            grpDetails.IsEnabled = false;
            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
        }
        private void NewEditSituation()
        {
            grpProducts.IsEnabled = false;
            grpDetails.IsEnabled = true;
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
        }
        private void PopulateListbox()
        {
            lstProducts.ItemsSource = null;
            lstProducts.ItemsSource = globalService.Products;
        }
        private void ClearControls()
        {
            txtCode.Text = "";
            txtDescription.Text = "";
            cmbGroups.SelectedIndex = -1;
            cmbVATs.SelectedIndex = -1;
            txtNetPrice.Text = "";
            lblGrossPrice.Content = "";
        }

        private void LstProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearControls();
            if (lstProducts.SelectedItem != null)
            {
                PopulateControls((Product)lstProducts.SelectedItem);
            }
        }
        private void PopulateControls(Product product)
        {
            txtCode.Text = product.Code;
            txtDescription.Text = product.Description;
            cmbGroups.SelectedItem = product.ProductGroup;
            cmbVATs.SelectedItem = product.Vat;
            txtNetPrice.Text = product.NetPrice.ToString("€#,##0.00");
            lblGrossPrice.Content = product.GrossPrice.ToString("€#,##0.00");
        }
        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            NewEditSituation();
            isNew = true;
            ClearControls();
            // instructie hieronder om zeker te zijn dat de gebruiker zeker
            // een verpakking selecteert
            cmbGroups.SelectedIndex = 0;
            cmbVATs.SelectedIndex = 0;
            txtCode.Focus();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstProducts.SelectedItem != null)
            {
                NewEditSituation();
                isNew = false;
                // instructie hieronder noodzakelijk om € uit tekstvak te verwijderen
                txtNetPrice.Text = ((Product)lstProducts.SelectedItem).NetPrice.ToString();
                txtCode.Focus();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstProducts.SelectedItem != null)
            {
                if (MessageBox.Show("Ben je zeker ?", "Wissen", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    globalService.DeleteProduct((Product)lstProducts.SelectedItem);
                    PopulateListbox();
                    ClearControls();
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string code = txtCode.Text;
            string description = txtDescription.Text;
            Group group = (Group)cmbGroups.SelectedItem;
            VAT vat = (VAT)cmbVATs.SelectedItem;
            decimal.TryParse(txtNetPrice.Text, out decimal netprice);
            Product product;
            if (isNew)
            {
                product = new Product(code, description,netprice, vat, group);
                globalService.AddProduct(product);
            }
            else
            {
                product = (Product)lstProducts.SelectedItem;
                product.Code = code;
                product.Description = description;
                product.NetPrice = netprice;
                product.Vat = vat;
                product.ProductGroup = group;
            }
            PopulateListbox();
            DefaultSituation();
            lstProducts.SelectedItem = product;
            LstProducts_SelectionChanged(null, null);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DefaultSituation();
            LstProducts_SelectionChanged(null, null);
        }
    }
}
