using ProElection.Persistence;

namespace ProElection.Repositories;

public class VoteRepository
{
    private readonly ProElectionDbContext _dbContext;

    public VoteRepository(ProElectionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}