using ProElection.Entities;

namespace ProElection.Services.Interfaces;

public interface IElectionService
{
    /// <summary>
    /// Retrieves elections with the given IDs.
    /// </summary>
    /// <param name="electionIds"><see cref="IEnumerable{Guid}"/> of election Ids</param>
    /// <returns><see cref="IEnumerable{Election}"/> associated with Ids given</returns>
    public Task<IEnumerable<Election>> GetElectionsByMultipleIds(IEnumerable<Guid> electionIds);

    /// <summary>
    /// Gets all existing elections.
    /// </summary>
    /// <returns><see cref="IEnumerable{Election}"/> containing all elections.</returns>
    public Task<IEnumerable<Election>> GetAllElections();
}