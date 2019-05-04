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
    public partial class EmployeePage : UserControl
    {

        private Employee selectedEmployee = new Employee();
        private ObservableCollection<Employee> employees = new ObservableCollection<Employee>();

        public EmployeePage()
        {
            InitializeComponent();
            GetEmployees();
        }
        //Gets all the current employees
        private void GetEmployees()
        {
            employees = DbServices.employeesDbService.Select();
            EmployeesGridView.ItemsSource = employees;
        }
        //When an employee is clicked on in the grid view, this method is called and assigns the data to both the selectedEmployee object and the UI fields
        private void EmployeesGridView_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            selectedEmployee = (Employee)EmployeesGridView.SelectedItem;
            FirstNameTextBox.Text = selectedEmployee.FirstName;
            LastNameTextBox.Text = selectedEmployee.LastName;
            TitleTextBox.Text = selectedEmployee.Title;
            SalaryNumeric.Value = selectedEmployee.Salary;
        }
        //Gets all the current employees
        private void ReadEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            GetEmployees();
        }
        //This method updates the selectedEmployee object with user entered data
        private void UpdateSelectedEmployee()
        {
            selectedEmployee.FirstName = FirstNameTextBox.Text;
            selectedEmployee.LastName = LastNameTextBox.Text;
            selectedEmployee.Title = TitleTextBox.Text;
            selectedEmployee.Salary = (int)SalaryNumeric.Value;
        }
        //This method attempts to create an employee
        private void CreateEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateSelectedEmployee();

            if (DbServices.employeesDbService.Insert(selectedEmployee))
            {
                MessageBox.Show("Employee Created!");
            }
            else
            {
                MessageBox.Show("An error occured while creating your employee.");
            }
        }
        //This method attempts to update an employee
        private void UpdateEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateSelectedEmployee();

            if (DbServices.employeesDbService.Update(selectedEmployee))
            {
                MessageBox.Show("Employee Updated!");
            }
            else
            {
                MessageBox.Show("An error occured while updating your employee.");
            }
        }
        //This method attempts to delete an employee
        private void DeleteEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateSelectedEmployee();

            if (DbServices.employeesDbService.Delete(selectedEmployee))
            {
                MessageBox.Show("Employee Deleted!");
            }
            else
            {
                MessageBox.Show("An error occured while deleting your employee.");
            }
        }
    }
}
