namespace be.Helpers
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; } = string.Empty;
    }
}