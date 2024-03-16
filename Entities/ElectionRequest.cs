using ProElection.Entities.Enums;
namespace ProElection.Entities;

public class ElectionRequest : UserElectionAssociation
{
    public RequestStatus Status { get; set; }
}