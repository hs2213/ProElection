using ProElection.Entities.Enums;

namespace ProElection.Entities;

public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
    public required string Address { get; set; }
    public required string Postcode { get; set; }
    public required string Country { get; set; }
    public required string HashedPassword { get; set; }
    
    /// <summary>
    /// Each user has a unique salt to hash their password with.
    /// Used to make rainbow table attacks more difficult.
    /// </summary>
    public required string PasswordSalt { get; set; }
    
    public UserType UserType { get; set; }
    
    public required List<Guid> ParticipatingElections { get; set; }
}