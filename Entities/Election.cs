using System.ComponentModel.DataAnnotations;
using ProElection.Entities.Enums;

namespace ProElection.Entities;

public class Election
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; } 
    public required List<Guid> Candidates { get; set; }
    
    public ElectionType ElectionType { get; set; }
}