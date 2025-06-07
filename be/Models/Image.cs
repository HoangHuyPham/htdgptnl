namespace be.Models
{
    public class Image
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Url { get; set; } = String.Empty;
        public float Size { get; set; } = 0;
        public Guid? EvidenceId { get; set; }
        public Evidence? Evidence { get; set;}
    }
}