using Raven.Client.Documents;
using RavenDbDemo.Models;

namespace RavenDbDemo.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDocumentStore _store;

        public CustomerRepository(IDocumentStore store)
        {
            _store = store;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            using var session = _store.OpenAsyncSession();

            return await session
                .Query<Customer>()
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(string id)
        {
            using var session = _store.OpenAsyncSession();

            return await session.LoadAsync<Customer>(id);
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            using var session = _store.OpenAsyncSession();

            customer.CreatedAt = DateTime.UtcNow;

            await session.StoreAsync(customer);

            await session.SaveChangesAsync();

            return customer;
        }

        public async Task<bool> UpdateAsync(
            string id,
            Customer customer)
        {
            using var session = _store.OpenAsyncSession();

            var existing = await session.LoadAsync<Customer>(id);

            if (existing == null)
                return false;

            existing.Name = customer.Name;
            existing.Email = customer.Email;
            existing.Phone = customer.Phone;

            await session.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            using var session = _store.OpenAsyncSession();

            var customer = await session.LoadAsync<Customer>(id);

            if (customer == null)
                return false;

            session.Delete(customer);

            await session.SaveChangesAsync();

            return true;
        }
    }
}