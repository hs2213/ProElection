namespace ProElection.Entities;

public class InPersonCode
{
    public Guid Code { get; set; }
    public Guid UserId { get; set; }
    public Guid ElectionId { get; set; }
}