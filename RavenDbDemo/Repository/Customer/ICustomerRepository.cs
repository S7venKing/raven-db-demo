using RavenDbDemo.Models;
using RavenDbDemo.Models.Customers;

namespace RavenDbDemo.Repositories
{
    public interface ICustomerRepository
    {
        Task<PagedResult<Customer>> GetAllAsync(CustomerFilter filter);

        Task<Customer?> GetByIdAsync(string id);

        Task<Customer?> GetByEmailAsync(string email);

        Task<Customer> CreateAsync(Customer customer);

        Task<bool> UpdateAsync(string id, Customer customer);

        Task<bool> DeleteAsync(string id);
    }
}