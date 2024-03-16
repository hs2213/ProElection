using ProElection.Entities;

namespace ProElection.Repositories.Interfaces;

/// <summary>
/// Repository containing methods for interacting with the Election Code table in the database.
/// </summary>
public interface IElectionCodeRepository
{
    /// <summary>
    /// Creates a new Election Code in the Election Code table in the database.
    /// </summary>
    /// <param name="electionCode"><see cref="ElectionCode"/> to add.</param>
    /// <returns><see cref="ElectionCode"/> added to the database.</returns>
    public Task<ElectionCode> CreateAsync(ElectionCode electionCode);

    /// <summary>
    /// Gets an Election Code by its ID from the Election Code table in the database.
    /// </summary>
    /// <param name="id"><see cref="Guid"/> Id of election code to get.</param>
    /// <returns><see cref="ElectionCode"/> with the given id or null if not found.</returns>
    public Task<ElectionCode?> GetAsync(Guid id);

    /// <summary>
    /// Updates an Election Code in the Election Code table in the database.
    /// </summary>
    /// <param name="electionCode"><see cref="ElectionCode"/> with details to update</param>
    /// <returns></returns>
    public Task UpdateAsync(ElectionCode electionCode);
}