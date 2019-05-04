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
using WpfCrud.Models;
using WpfCrud.Services;

namespace WpfCrud.UserControls
{
    public partial class ProductPage : UserControl
    {
        //selectedProduct is the object referenced for all CRUD operations except for Read
        private Product selectedProduct = new Product();
        //Im using an ObservableCollection because it inherits InotifyPropertyChanged
        private ObservableCollection<Product> products = new ObservableCollection<Product>();

        public ProductPage()
        {
            InitializeComponent();
            //Reads on initialization
            GetProducts();
        }

        private void GetProducts() //retrieves all the Products
        {
            products = DbServices.productsDbService.Select();
            ProductsGridView.ItemsSource = products;
        }

        //This handler is fired when a user clicks on a row on the GridView and makes that product the current reference
        private void ProductsGridView_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            selectedProduct = (Product)ProductsGridView.SelectedItem;
            NameTextBox.Text = selectedProduct.Name;
            PriceNumeric.Value = selectedProduct.Price;
            ReleaseDateDateTime.SelectedDate = selectedProduct.ReleaseDate;
        }

        private void ReadProductsButton_Click(object sender, RoutedEventArgs e)
        {
            GetProducts();
        }
        //pulls the data from the GUI and updates the properties of the product to be referenced in CRUD Operations
        private void UpdateSelectedProduct()
        {
            selectedProduct.Name = NameTextBox.Text;
            selectedProduct.Price = (double)PriceNumeric.Value;
            selectedProduct.ReleaseDate = Convert.ToDateTime(ReleaseDateDateTime.SelectedDate);
        }
        //Attempts to create a new product in MySQL
        private void CreateProductButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateSelectedProduct();
            if (DbServices.productsDbService.Insert(selectedProduct))
            {
                MessageBox.Show("Product Created!");
            }
            else
            {
                MessageBox.Show("An error occured while creating your Product.");
            }
        }
        //Updates a row in MySQL
        private void UpdateProductButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateSelectedProduct();
            if (DbServices.productsDbService.Update(selectedProduct))
            {
                MessageBox.Show("Product Updated!");
            }
            else
            {
                MessageBox.Show("An error occured while updating your Product.");
            }
        }
        //Deletes a Product
        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateSelectedProduct();

            if (DbServices.productsDbService.Delete(selectedProduct))
            {
                MessageBox.Show("Product Deleted!");
            }
            else
            {
                MessageBox.Show("An error occured while deleting your Product.");
            }
        }
    }
}
