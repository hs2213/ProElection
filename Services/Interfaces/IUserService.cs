using ProElection.Entities;
using ProElection.Entities.Enums;

namespace ProElection.Services.Interfaces;

/// <summary>
/// Service to handle user related operations.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Used to authenticate a user by their email and password.
    /// </summary>
    /// <param name="email">input user email</param>
    /// <param name="password">raw input user password</param>
    /// <returns>null or <see cref="User"/> if email exists in the db and the password is correct.</returns>
    public Task<User?> Authenticate(string email, string password);

    /// <summary>
    /// Gets a user by its Id.
    /// </summary>
    /// <param name="id"><see cref="Guid"/> Id of user to get.</param>
    /// <returns><see cref="User"/> with the given ID or null if not found.</returns>
    public Task<User?> GetUserById(Guid id);

    /// <summary>
    /// Gets all the users with the type of <see cref="UserType.Candidate"/>
    /// </summary>
    /// <returns>List of <see cref="User"/> of type <see cref="UserType.Candidate"/></returns>
    public IEnumerable<User> GetCandidates();

    /// <summary>
    /// Creates a new user, hashing its password.
    /// </summary>
    /// <param name="user"><see cref="User"/> to add to the database.</param>
    /// <returns>null if the email of the user is not unique or
    /// <see cref="User"/> added to the database</returns>
    public Task<User?> CreateUser(User user);
}