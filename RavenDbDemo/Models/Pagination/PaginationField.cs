namespace RavenDbDemo.Models.Pagination
{
    public class PaginationField
    {
        public string Keyword { get; set; } = string.Empty;

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 20;

        public string SortBy { get; set; } = "CreatedAt";

        public bool SortDescending { get; set; } = true;
    }
}
