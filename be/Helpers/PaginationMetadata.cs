namespace be.Helpers
{
    public class PaginationMetadata
    {
        public int Total { get; set; } = 0;
        public int Page { get; set;} = 0;
        public int Limit { get; set; } = 0;
        public string Sort { get; set; } = String.Empty;
    }
}