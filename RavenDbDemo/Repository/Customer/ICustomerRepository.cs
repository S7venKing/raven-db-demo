using RavenDbDemo.Models;

namespace RavenDbDemo.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();

        Task<Customer?> GetByIdAsync(string id);

        Task<Customer> CreateAsync(Customer customer);

        Task<bool> UpdateAsync(string id, Customer customer);

        Task<bool> DeleteAsync(string id);
    }
}