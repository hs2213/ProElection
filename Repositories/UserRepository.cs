using ProElection.Persistence;
using ProElection.Repositories.Interfaces;

namespace ProElection.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ProElectionDbContext _dbContext;

    public UserRepository(ProElectionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}