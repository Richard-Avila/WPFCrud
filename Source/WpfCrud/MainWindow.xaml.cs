using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using WpfCrud.UserControls;

namespace WpfCrud
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HomePage homePage = new HomePage();
        ProductPage productPage = new ProductPage();
        EmployeePage employeePage = new EmployeePage();

        public MainWindow()
        {
            InitializeComponent();
            MainDisplay.Content = homePage;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ExitProgram();
        }
        //These methods change which user control is displayed
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainDisplay.Content = homePage;
        }

        private void ProductsButton_Click(object sender, RoutedEventArgs e)
        {
            MainDisplay.Content = productPage;
        }

        private void EmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            MainDisplay.Content = employeePage;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            ExitProgram();
        }
        //I kind of do this out of habit, I generally do a lot of multithreading and if those threads are not killed the program will not close properly
        private void ExitProgram()
        {
            App.Current.Shutdown();
            Process.GetCurrentProcess().Kill();
        }
    }
}
