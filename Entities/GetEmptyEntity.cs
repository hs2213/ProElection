namespace ProElection.Entities;

/// <summary>
/// Gets an empty version of an entity for initialisation
/// </summary>
public static class GetEmptyEntity
{
    public static User User()
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Address = string.Empty,
            Country = string.Empty,
            Email = string.Empty,
            HashedPassword = string.Empty,
            PasswordSalt = string.Empty,
            ParticipatingElections = [],
            Name = string.Empty,
            PhoneNumber = string.Empty,
            Postcode = string.Empty
        }; 
    }
    
    
}