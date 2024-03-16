namespace ProElection.Entities;

public class UserElectionAssociation
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ElectionId { get; set; }
}