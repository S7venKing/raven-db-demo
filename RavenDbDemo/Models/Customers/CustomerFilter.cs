using RavenDbDemo.Models.Pagination;

namespace RavenDbDemo.Models.Customers
{
    public class CustomerFilter: PaginationField
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }
    }
}
