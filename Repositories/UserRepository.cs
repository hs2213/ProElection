using Microsoft.EntityFrameworkCore;
using ProElection.Entities;
using ProElection.Entities.Enums;
using ProElection.Persistence;
using ProElection.Repositories.Interfaces;

namespace ProElection.Repositories;

/// <inheritdoc/>
public class UserRepository : IUserRepository
{
    private readonly ProElectionDbContext _dbContext;

    public UserRepository(ProElectionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <inheritdoc/>
    public async Task<User?> GetUserById(Guid id)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(user => user.Id == id);
    }
    
    /// <inheritdoc/>
    public async Task<User?> GetUserByEmail(string email)
    {
        return await _dbContext.Users
            .SingleOrDefaultAsync(user => user.Email == email);
    }
    
    /// <inheritdoc/>
    public async Task<User> CreateUser(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }
    
    /// <inheritdoc/>
    public async Task<IEnumerable<User>> GetCandidates()
    {
        return await _dbContext.Users.Where(user => user.UserType == UserType.Candidate).ToListAsync();
    }
    
    /// <inheritdoc/>
    public async Task<bool> CheckEmailExists(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email == email);
    }
    
    /// <inheritdoc/>
    public async Task UpdateUser(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }
}