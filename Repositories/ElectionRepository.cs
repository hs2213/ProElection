using ProElection.Persistence;

namespace ProElection.Repositories;

public class ElectionRepository
{
    private readonly ProElectionDbContext _dbContext;

    public ElectionRepository(ProElectionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}