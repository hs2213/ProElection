using ProElection.Entities;

namespace ProElection.Repositories.Interfaces;

/// <summary>
/// Repository containing methods for interacting with the elections table in the database.
/// </summary>
public interface IElectionRepository
{
    /// <summary>
    /// Gets all existing elections in the elections table in the database.
    /// </summary>
    /// <returns>The newly created election</returns>
    public Task<IEnumerable<Election>> GetElections();

    /// <summary>
    /// Gets an election by its ID from the elections table in the database.
    /// </summary>
    /// <returns><see cref="Election"/> with the given ID or null if not found.</returns>
    public Task<Election?> GetElectionById(Guid id);

    /// <summary>
    /// Creates an election in the elections table in the database.
    /// </summary>
    /// <param name="election"><see cref="Election"/> to add to the database.</param>
    /// <returns>The newly created election</returns>
    public Task<Election> CreateElection(Election election);

    /// <summary>
    /// Updates an election in the elections table in the database.
    /// </summary>
    /// <param name="election"><see cref="Election"/> to update.</param>
    /// <returns></returns>
    public Task UpdateElection(Election election);
}