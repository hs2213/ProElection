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
    public required string HashedPassword { get; set; }
    public Country Country { get; set; }
    public UserType UserType { get; set; }
}