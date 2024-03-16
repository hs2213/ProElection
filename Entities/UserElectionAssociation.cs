namespace ProElection.Entities;

public abstract class UserElectionAssociation
{
    public Guid Id { get; set; }
    public Guid ElectionId { get; set; }
    public Guid UserId { get; set; }
}