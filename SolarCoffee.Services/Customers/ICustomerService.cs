using System;
using System.Collections.Generic;
using System.Text;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Customers
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();
        ServiceResponse<Customer> CreateCustomer(Customer customer);
        ServiceResponse<bool> DeleteCustomer(int id);
        Customer GetById(int id);
    }
}
