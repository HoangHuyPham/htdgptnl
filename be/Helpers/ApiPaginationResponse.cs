namespace be.Helpers
{
    public class ApiPaginationResponse<T> : ApiResponse<T>
    {
        public PaginationMetadata Pagination { get; set; } = new PaginationMetadata();
    }
}