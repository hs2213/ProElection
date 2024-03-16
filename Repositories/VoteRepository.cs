using ProElection.Persistence;
using ProElection.Repositories.Interfaces;

namespace ProElection.Repositories;

public class VoteRepository : IVoteRepository
{
    private readonly ProElectionDbContext _dbContext;

    public VoteRepository(ProElectionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}