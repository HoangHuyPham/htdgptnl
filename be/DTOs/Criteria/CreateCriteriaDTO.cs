namespace be.DTOs.Criteria
{
    public class CreateCriteriaDTO
    {
        public string Content { get; set; } = String.Empty;
        public bool EvidenceRequired { get; set; } = false;
        public Guid? AchievementItemId { get; set; }
    }
}