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
    public class ProductsDbService
    {
        private MySqlConnection connection;
        
        public ProductsDbService()
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
        public bool Insert(Product product)
        {
            try
            {
                //open connection
                if (this.OpenConnection() == true)
                {
                    //create sql command
                    MySqlCommand cmd = new MySqlCommand("Insert Into DemoDB.Products (Name, Price, ReleaseDate) Values (?name, ?price, ?releasedate)", connection);
                    //Its ideal to use parameters to avoid SQL injections
                    cmd.Parameters.AddWithValue("?name", product.Name);
                    cmd.Parameters.AddWithValue("?price", product.Price);
                    cmd.Parameters.AddWithValue("?releasedate", product.ReleaseDate);

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
        public bool Update(Product product)
        {
            try
            {
                //Open connection
                if (this.OpenConnection() == true)
                {
                    //create mysql command
                    MySqlCommand cmd = new MySqlCommand("UPDATE DemoDB.Products SET Name = ?name, Price = ?price, ReleaseDate = ?releasedate WHERE ID = ?id" ,connection);

                    cmd.Parameters.AddWithValue("?name", product.Name);
                    cmd.Parameters.AddWithValue("?price", product.Price);
                    cmd.Parameters.AddWithValue("?releasedate", product.ReleaseDate);
                    cmd.Parameters.AddWithValue("?id", product.ID);

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
        public bool Delete(Product product) //deletes the coffee from the DB
        {
            try
            {
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM DemoDB.Products WHERE ID = ?id", connection);
                    cmd.Parameters.AddWithValue("?id", product.ID);
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
        public ObservableCollection<Product> Select() //returns a list of all the Products
        {
            string query = "SELECT * FROM DemoDB.Products";
            //Collection of Products (Observables are nice since they inherit the INotifyPropertyChanged class)
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create the data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    //creates a Product object
                    Product product = new Product();

                    //sets the products's properties
                    product.ID = Convert.ToInt32(dataReader["ID"]);
                    product.Name = Convert.ToString(dataReader["Name"]);
                    product.Price = Convert.ToDouble(dataReader["Price"]);
                    product.ReleaseDate = Convert.ToDateTime(dataReader["ReleaseDate"]);
                    //adds the product to the collection of products
                    products.Add(product);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return collection to be displayed
                return products;
            }
            else
            {
                return null;
            }
        }
        
    }
}