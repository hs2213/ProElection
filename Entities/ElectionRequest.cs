using ProElection.Entities.Enums;
namespace ProElection.Entities;

public class ElectionRequest : UserElectionAssociation
{
    public UserType UserType { get; set; }
    public RequestStatus Status { get; set; }
}