using ProElection.Entities.Enums;

namespace ProElection.Entities;

public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Country Country { get; set; }
}