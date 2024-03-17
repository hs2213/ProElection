using Microsoft.EntityFrameworkCore;
using ProElection.Entities;
using ProElection.Persistence;
using ProElection.Repositories.Interfaces;

namespace ProElection.Repositories;

/// <inheritdoc/>
public class ElectionRepository : IElectionRepository
{
    private readonly ProElectionDbContext _dbContext;

    public ElectionRepository(ProElectionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <inheritdoc/>
    public async Task<IEnumerable<Election>> GetElections()
    {
        return await _dbContext.Elections.ToListAsync();
    }

    public async Task<IEnumerable<Election>> GetMultipleElectionsByIds(IEnumerable<Guid> ids)
    {
        return await _dbContext.Elections.Where(election => ids.Contains(election.Id)).ToListAsync();
    }
    
    /// <inheritdoc/>
    public async Task<Election?> GetElectionById(Guid id)
    {
        return await _dbContext.Elections.SingleOrDefaultAsync(election => election.Id == id);
    }
    
    /// <inheritdoc/>
    public async Task<Election> CreateElection(Election election)
    {
        await _dbContext.Elections.AddAsync(election);
        await _dbContext.SaveChangesAsync();
        return election;
    }
    
    /// <inheritdoc/>
    public async Task UpdateElection(Election election)
    {
        _dbContext.Elections.Update(election);
        await _dbContext.SaveChangesAsync();
    }
}