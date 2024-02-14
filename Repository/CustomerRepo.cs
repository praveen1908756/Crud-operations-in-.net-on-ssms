using api2.Models;
using api2.Repository.interfaces;
using System.Data.SqlClient;

namespace api2.Repository
{
    public class CustomerRepo : ICustomerRepo
    {
        string connectionString = "";

        public CustomerRepo()
        {
            connectionString = @"Data Source=APINP-ELPTSSU5O\SQLEXPRESS;Initial Catalog=northwind;Persist Security Info=True;User ID=tap2023;Password=tap2023;Encrypt=False";
        }

        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Customers";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Customer c = new Customer();
                    c.CustomerId = rdr["CustomerID"].ToString();
                    c.CompanyName = rdr["CompanyName"].ToString();
                    c.ContactName = rdr["ContactName"].ToString();

                    customers.Add(c);
                }
            }
            return customers;
        }

        public Customer GetCustomerById(string id)
        {
            Customer c = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM Customers WHERE CustomerId='{id}'";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    c = new Customer();
                    c.CustomerId = rdr["CustomerID"].ToString();
                    c.CompanyName = rdr["CompanyName"].ToString();
                    c.ContactName = rdr["ContactName"].ToString();
                }
            }
            return c;
        }

        public Customer CreateCustomer(Customer customer)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Customers (CustomerId, CompanyName, ContactName) VALUES (@CustomerId, @CompanyName, @ContactName);";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerId);
                cmd.Parameters.AddWithValue("@CompanyName", customer.CompanyName);
                cmd.Parameters.AddWithValue("@ContactName", customer.ContactName);

                con.Open();
                cmd.ExecuteNonQuery();
            }
            return customer;
        }

        public Customer UpdateCustomer(Customer customer)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Customers SET CompanyName = @CompanyName, ContactName = @ContactName WHERE CustomerId = @CustomerId;";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerId);
                cmd.Parameters.AddWithValue("@CompanyName", customer.CompanyName);
                cmd.Parameters.AddWithValue("@ContactName", customer.ContactName);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("No records updated");
                }
            }
            return customer;
        }

        public bool DeleteCustomer(string id)
        {
            bool result = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string deleteCustomerQueryOrders = $"DELETE FROM Orders WHERE CustomerID = '{id}';";
                string deleteCustomerQueryOrderDetails = $"DELETE FROM [Order Details] WHERE OrderID IN (SELECT OrderID FROM Orders WHERE CustomerID = '{id}');";
                string deleteCustomerQueryCustomers = $"DELETE FROM Customers WHERE CustomerID = '{id}';";
                SqlCommand cmd = new SqlCommand(deleteCustomerQueryOrderDetails, con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(deleteCustomerQueryOrders, con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(deleteCustomerQueryCustomers, con);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}