using Microsoft.EntityFrameworkCore;
using ProElection.Entities;
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
    
    /// <inheritdoc/>
    public async Task<IEnumerable<Election>> GetElectionsAsync()
    {
        return await _dbContext.Elections.ToListAsync();
    }
    
    /// <inheritdoc/>
    public async Task<Election?> GetElectionByIdAsync(Guid id)
    {
        return await _dbContext.Elections.SingleOrDefaultAsync(election => election.Id == id);
    }
    
    /// <inheritdoc/>
    public async Task<Election> CreateElectionAsync(Election election)
    {
        await _dbContext.Elections.AddAsync(election);
        await _dbContext.SaveChangesAsync();
        return election;
    }
    
    /// <inheritdoc/>
    public async Task UpdateElectionAsync(Election election)
    {
        _dbContext.Elections.Update(election);
        await _dbContext.SaveChangesAsync();
    }
}