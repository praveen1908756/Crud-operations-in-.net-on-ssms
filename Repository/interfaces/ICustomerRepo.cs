using api2.Models;

namespace api2.Repository.interfaces
{
    public interface ICustomerRepo
    {
        List<Customer> GetCustomers();
        Customer GetCustomerById(string id);
        Customer CreateCustomer(Customer customer);
        Customer UpdateCustomer(Customer customer);
        bool DeleteCustomer(string id);
    }
}