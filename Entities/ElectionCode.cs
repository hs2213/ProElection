using ProElection.Entities.Enums;

namespace ProElection.Entities;

public class ElectionCode
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid ElectionId { get; set; }
    
    public CodeStatus Status { get; set; }
}