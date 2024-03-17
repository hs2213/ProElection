using ProElection.Entities.Enums;

namespace ProElection.Entities;

/// <summary>
/// Election code entity that is used for in person voting.
/// </summary>
public class ElectionCode : UserElectionAssociation
{
    /// <summary>
    /// Marker to show if the code has already been used or not.
    /// </summary>
    public CodeStatus Status { get; set; }
}