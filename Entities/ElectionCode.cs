using ProElection.Entities.Enums;

namespace ProElection.Entities;

public class ElectionCode : UserElectionAssociation
{
    public CodeStatus Status { get; set; }
}