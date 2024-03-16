using Microsoft.EntityFrameworkCore;
using ProElection.Entities;
using ProElection.Persistence;
using ProElection.Repositories.Interfaces;

namespace ProElection.Repositories;

/// <inheritdoc/>
public class ElectionCodeRepository : IElectionCodeRepository
{
    private readonly ProElectionDbContext _dbContext;

    public ElectionCodeRepository(ProElectionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <inheritdoc/>
    public async Task<ElectionCode> CreateAsync(ElectionCode electionCode)
    {
        await _dbContext.ElectionCodes.AddAsync(electionCode);
        await _dbContext.SaveChangesAsync();
        return electionCode;
    }
    
    /// <inheritdoc/>
    public async Task<ElectionCode?> GetAsync(Guid id)
    {
        return await _dbContext.ElectionCodes.SingleOrDefaultAsync(storedCode => storedCode.Id == id);
    }
    
    /// <inheritdoc/>
    public async Task UpdateAsync(ElectionCode electionCode)
    {
        _dbContext.ElectionCodes.Update(electionCode);
        await _dbContext.SaveChangesAsync();
    }
}