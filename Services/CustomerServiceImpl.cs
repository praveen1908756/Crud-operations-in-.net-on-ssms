using api2.Models;
using api2.Repository.interfaces;
using api2.Services.Interfaces;

namespace api2.Services
{
    public class CustomerServiceImpl : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomerServiceImpl(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public List<Customer> GetCustomers()
        {
            return _customerRepo.GetCustomers();
        }

        public Customer GetCustomerById(string id)
        {
            return _customerRepo.GetCustomerById(id);
        }

        public Customer CreateCustomer(string id, string companyName, string contactName)
        {
            var customer = MapCustomerDTOToCustomerCreate(id, companyName, contactName);
            return _customerRepo.CreateCustomer(customer);
        }

        public Customer UpdateCustomer(string id, string companyName, string contactName)
        {
            var customer = MapCustomerDTOToCustomerUpdate(id, companyName, contactName);
            return _customerRepo.UpdateCustomer(customer);
        }

        public bool DeleteCustomer(string id)
        {
            return _customerRepo.DeleteCustomer(id);
        }

        private static Customer MapCustomerDTOToCustomerCreate(string id, string companyName, string contactName)
        {
            return new Customer
            {
                CustomerId = id,
                CompanyName = companyName,
                ContactName = contactName
            };
        }
        private static Customer MapCustomerDTOToCustomerUpdate(string id, string companyName, string contactName)
        {
            return new Customer
            {
                CustomerId = id,
                CompanyName = companyName,
                ContactName = contactName
            };
        }
    }
}