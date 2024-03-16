namespace ProElection.Entities;

public class Vote : UserElectionAssociation
{
    public Guid CandidateId { get; set; }
    public DateTimeOffset Time { get; set; }
}