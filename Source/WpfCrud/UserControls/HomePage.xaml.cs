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
    public partial class HomePage : UserControl
    {
        //The GridViews bind to these observable collections
        private ObservableCollection<Product> products = new ObservableCollection<Product>();
        private ObservableCollection<Employee> employees = new ObservableCollection<Employee>();

        public HomePage()
        {
            InitializeComponent();
            //Populates the grid views on initialization
            GetEmployees();
            GetProducts();
        }
        //Gets all the current products
        private void RefreshProductsButton_Click(object sender, RoutedEventArgs e)
        {
            GetProducts();
        }
        //Gets all the current employees
        private void RefreshEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            GetEmployees();
        }
        //Gets all the current products
        private void GetProducts()
        {
            products = DbServices.productsDbService.Select();
            ProductsGridView.ItemsSource = products;
        }
        //Gets all the current employees
        private void GetEmployees()
        {
            employees = DbServices.employeesDbService.Select();
            EmployeesGridView.ItemsSource = employees;
        }
    }
}
