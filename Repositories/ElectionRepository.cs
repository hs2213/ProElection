using ProElection.Persistence;
using ProElection.Repositories.Interfaces;

namespace ProElection.Repositories;

public class ElectionRepository : IElectionRepository
{
    private readonly ProElectionDbContext _dbContext;

    public ElectionRepository(ProElectionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}