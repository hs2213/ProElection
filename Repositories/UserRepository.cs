using ProElection.Persistence;

namespace ProElection.Repositories;

public class UserRepository
{
    private readonly ProElectionDbContext _dbContext;

    public UserRepository(ProElectionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}