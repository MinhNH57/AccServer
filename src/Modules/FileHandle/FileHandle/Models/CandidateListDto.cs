namespace FileHandle.Models;

public class CandidateListDto
{
    public Guid Id { get; set; }
    public string? CandidateCode { get; set; }
    public string? NameObj { get; set; }
    public string? CodeObj { get; set; }
    public string? Email { get; set; }
    public string? AddressObj { get; set; }
    public string? Numberphone { get; set; }
    public string? Gender { get; set; }
    public string? CodeRecruitmentSourcre { get; set; }
    public string? NameRecruitmentSourcre { get; set; }
    public string? CodeRecruimentCampaign { get; set; }
    public string? NameRecruimentCampaign { get; set; }
}
