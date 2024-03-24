﻿using ProElection.Entities;
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
    public Task<User?> GetUserById(Guid id);

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
    public Task<User> CreateUser(User user);

    /// <summary>
    /// Gets all the users with the type of <see cref="UserType.Candidate"/>
    /// </summary>
    /// <returns>List of candidates in the database.</returns>
    public Task<IEnumerable<User>> GetCandidates();
    
    /// <summary>
    /// Checks if an email already exists in the database.
    /// </summary>
    /// <param name="email">Email to check</param>
    public Task<bool> CheckEmailExists(string email);

    /// <summary>
    /// Updates a user in the database.
    /// </summary>
    /// <param name="user"><see cref="User"/> to update.</param>
    /// <returns></returns>
    public Task UpdateUser(User user);

    /// <summary>
    /// Gets all the candidates of an election.
    /// </summary>
    /// <param name="electionId">Id of election to get candidates of</param>
    /// <returns><see cref="IEnumerable{User}"/> containing the candidates of the election.</returns>
    public Task<IEnumerable<User>> GetCandidatesOfAnElection(Guid electionId);

    /// <summary>
    /// Gets all the users that are not already part of the given election,
    /// where their email contains the search query filtering by the <see cref="UserType"/>.
    /// </summary>
    /// <param name="searchQuery">email query to search for</param>
    /// <param name="userType"><see cref="UserType"/> of user being searched for</param>
    /// <param name="electionId">Id of the election to filter out users that arent already a part of</param>
    /// <returns>A list of users that can be added to an election</returns>
    public Task<IEnumerable<User>> GetUserBySearchForElection(string searchQuery, UserType userType, Guid electionId);
}