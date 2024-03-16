namespace ProElection.Entities;

public class Vote
{
    public Guid Id { get; set; }
    public Guid ElectionId { get; set; }
    public Guid CandidateId { get; set; }
    public DateTimeOffset Time { get; set; }
}