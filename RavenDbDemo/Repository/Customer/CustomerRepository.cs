using Raven.Client.Documents;
using RavenDbDemo.Models;
using RavenDbDemo.Models.Customers;

namespace RavenDbDemo.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IDocumentStore _store;

    public CustomerRepository(IDocumentStore store)
    {
        _store = store;
    }

    public async Task<PagedResult<Customer>> GetAllAsync(
        CustomerFilter filter)
    {
        using var session = _store.OpenAsyncSession();

        // Validate paging
        var page = filter.Page < 1
            ? 1
            : filter.Page;

        var pageSize = filter.PageSize switch
        {
            < 1 => 20,
            > 100 => 100,
            _ => filter.PageSize
        };

        var query = session
            .Query<Customer>()
            .AsQueryable();

        // ============================
        // FILTER
        // ============================

        if (!string.IsNullOrWhiteSpace(filter.Keyword))
        {
            var keyword = filter.Keyword.Trim();

            query = query.Where(x =>
                x.Name.Contains(keyword) ||
                x.Email.Contains(keyword) ||
                x.Phone.Contains(keyword));
        }

        if (!string.IsNullOrWhiteSpace(filter.Email))
        {
            var email = filter.Email.Trim();

            query = query.Where(x =>
                x.Email == email);
        }

        if (!string.IsNullOrWhiteSpace(filter.Phone))
        {
            var phone = filter.Phone.Trim();

            query = query.Where(x =>
                x.Phone == phone);
        }

        // ============================
        // COUNT
        // ============================

        var totalItems = await query.CountAsync();

        // ============================
        // SORT
        // ============================

        query = filter.SortBy.ToLowerInvariant() switch
        {
            "name" => filter.SortDescending
                ? query.OrderByDescending(x => x.Name)
                : query.OrderBy(x => x.Name),

            "email" => filter.SortDescending
                ? query.OrderByDescending(x => x.Email)
                : query.OrderBy(x => x.Email),

            "phone" => filter.SortDescending
                ? query.OrderByDescending(x => x.Phone)
                : query.OrderBy(x => x.Phone),

            "createdat" => filter.SortDescending
                ? query.OrderByDescending(x => x.CreatedAt)
                : query.OrderBy(x => x.CreatedAt),

            _ => query.OrderByDescending(x => x.CreatedAt)
        };

        // ============================
        // PAGINATION
        // ============================

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Customer>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems
        };
    }

    public async Task<Customer?> GetByIdAsync(string id)
    {
        using var session = _store.OpenAsyncSession();

        return await session.LoadAsync<Customer>(id);
    }

    public async Task<Customer?> GetByEmailAsync(
        string email)
    {
        using var session = _store.OpenAsyncSession();

        email = email.Trim();

        return await session
            .Query<Customer>()
            .FirstOrDefaultAsync(x =>
                x.Email == email);
    }

    public async Task<Customer> CreateAsync(
        Customer customer)
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

        var existing =
            await session.LoadAsync<Customer>(id);

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

        var existing =
            await session.LoadAsync<Customer>(id);

        if (existing == null)
            return false;

        session.Delete(existing);

        await session.SaveChangesAsync();

        return true;
    }
}