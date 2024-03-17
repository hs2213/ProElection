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
    public async Task<ElectionCode> Create(ElectionCode electionCode)
    {
        await _dbContext.ElectionCodes.AddAsync(electionCode);
        await _dbContext.SaveChangesAsync();
        return electionCode;
    }
    
    /// <inheritdoc/>
    public async Task<ElectionCode?> GetById(Guid id)
    {
        return await _dbContext.ElectionCodes.SingleOrDefaultAsync(storedCode => storedCode.Id == id);
    }
    
    public async Task<ElectionCode?> GetByElectionAndUser(Guid electionId, Guid userId)
    {
        return await _dbContext.ElectionCodes
            .SingleOrDefaultAsync(code => code.ElectionId == electionId && code.UserId == userId);
    }
    
    /// <inheritdoc/>
    public async Task Update(ElectionCode electionCode)
    {
        _dbContext.ElectionCodes.Update(electionCode);
        await _dbContext.SaveChangesAsync();
    }
}