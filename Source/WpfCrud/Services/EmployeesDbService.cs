using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCrud.Models;
using System.Windows;
using System.Configuration;

namespace WpfCrud.Services
{
    public class EmployeesDbService
    {
        private MySqlConnection connection;

        //Constructor
        public EmployeesDbService()
        {
            //This line pulls the connection string from App.Config
            connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlServer"].ConnectionString);
        }
        
        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                //I like to close the connection before opening a new one (unless I am in a loop that needs to be tight)
                //If a query has an error the connection will still be open afterwards, which opening an already open connection results in an error
                CloseConnection();
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;
                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Insert statement
        public bool Insert(Employee employee)
        {

            try
            {
                //open connection
                if (this.OpenConnection() == true)
                {
                    //create command and add the query and connection as parameters
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO DemoDB.Employees (FirstName, LastName, Title, Salary) VALUES (?firstname, ?lastname, ?title, ?salary)", connection);
                    //Add parameters to the query. Its important to use parameters to avoid SQL injections
                    cmd.Parameters.AddWithValue("?firstname", employee.FirstName);
                    cmd.Parameters.AddWithValue("?lastname", employee.LastName);
                    cmd.Parameters.AddWithValue("?title", employee.Title);
                    cmd.Parameters.AddWithValue("?salary", employee.Salary);
                    //Execute command
                    cmd.ExecuteNonQuery();
                    //close connection
                    this.CloseConnection();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Update statement
        public bool Update(Employee employee)
        {
            try
            {
                //Open connection
                if (this.OpenConnection() == true)
                {
                    //create mysql command
                    MySqlCommand cmd = new MySqlCommand("UPDATE DemoDB.Employees SET FirstName = ?firstname, LastName = ?lastname, Title = ?title, Salary = ?salary WHERE ID = ?id", connection);
                    cmd.Parameters.AddWithValue("?id", employee.ID);
                    cmd.Parameters.AddWithValue("?firstname", employee.FirstName);
                    cmd.Parameters.AddWithValue("?lastname", employee.LastName);
                    cmd.Parameters.AddWithValue("?title", employee.Title);
                    cmd.Parameters.AddWithValue("?salary", employee.Salary);
                    //Execute query
                    cmd.ExecuteNonQuery();
                    //close connection
                    this.CloseConnection();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Delete statement
        public bool Delete(Employee employee) //deletes the coffee from the DB
        {
            try
            {
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM DemoDB.Employees WHERE ID = ?id", connection);
                    cmd.Parameters.AddWithValue("?id", employee.ID);
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Select statement
        public ObservableCollection<Employee> Select() //returns a collection of all the Employees
        {
            string query = "SELECT * FROM DemoDB.Employees";
            //list of Employees
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create the data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the collection
                while (dataReader.Read())
                {
                    //creates the Employee object
                    Employee employee = new Employee();
                    //sets the Employee's properties
                    employee.ID = Convert.ToInt32(dataReader["ID"]);
                    employee.FirstName = Convert.ToString(dataReader["FirstName"]);
                    employee.LastName = Convert.ToString(dataReader["LastName"]);
                    employee.Title = Convert.ToString(dataReader["Title"]);
                    employee.Salary = Convert.ToInt32(dataReader["Salary"]);
                    //adds the employee to the collection
                    employees.Add(employee);
                }
                //close Data Reader
                dataReader.Close();
                //close Connection
                this.CloseConnection();
                //return the collection
                return employees;
            }
            else
            {
                return null;
            }
        }
    }
}