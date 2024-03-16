using ProElection.Entities.Enums;

namespace ProElection.Entities;

public class Election
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
    public Country Country { get; set; }
}