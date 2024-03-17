using ProElection.Entities;
using ProElection.Entities.Enums;

namespace ProElection.Repositories.Interfaces;

/// <summary>
/// Repository containing methods for interacting with the users table in the database.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Gets a user from the database by its Id.
    /// </summary>
    /// <param name="id"><see cref="Guid"/> Id of user to get.</param>
    /// <returns><see cref="User"/> with the given ID or null if not found.</returns>
    public Task<User?> GetUserByIdAsync(Guid id);

    /// <summary>
    /// Gets a user from the database by its email.
    /// </summary>
    /// <param name="email"><see cref="string"/> containing the user's email</param>
    /// <returns><see cref="User"/> with the given email or null if not found.</returns>
    public Task<User?> GetUserByEmail(string email);

    /// <summary>
    /// Creates a new user in the database.
    /// </summary>
    /// <param name="user"><see cref="User"/> to add to the database.</param>
    /// <returns><see cref="User"/> added to the database</returns>
    public Task<User> CreateUserAsync(User user);

    /// <summary>
    /// Gets all the users with the type of <see cref="UserType.Candidate"/>
    /// </summary>
    /// <returns>List of candidates in the database.</returns>
    public IEnumerable<User> GetCandidatesAsync();

    /// <summary>
    /// Checks if an email already exists in the database.
    /// </summary>
    /// <param name="email">Email to check</param>
    public Task<bool> CheckEmailExistsAsync(string email);
}