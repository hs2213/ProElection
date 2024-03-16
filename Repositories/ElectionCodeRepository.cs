using ProElection.Persistence;

namespace ProElection.Repositories;

public class ElectionCodeRepository
{
    private readonly ProElectionDbContext _dbContext;

    public ElectionCodeRepository(ProElectionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}