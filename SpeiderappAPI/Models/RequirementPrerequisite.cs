namespace SpeiderappAPI.Models
{
    public class RequirementPrerequisite
    {
        public RequirementPrerequisite(bool isAdvisory)
        {
            IsAdvisory = isAdvisory;
        }

        public long RequirerId { get; set; }
        public long RequireeId { get; set; }
        public Requirement Requirer { get; set; } = null!;
        public Requirement Requiree { get; set; } = null!;
        public bool IsAdvisory { get; set; }
    }
}
