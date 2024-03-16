using Microsoft.EntityFrameworkCore;
using ProElection.Entities;
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
    
    /// <inheritdoc/>
    public async Task CreateAsync(Vote vote)
    {
        await _dbContext.Votes.AddAsync(vote);
        await _dbContext.SaveChangesAsync();
    }
    
    /// <inheritdoc/>
    public async Task<bool> CheckIfUserVotedInElectionAsync(Guid userId, Guid electionId)
    {
        return await _dbContext.Votes.AnyAsync(vote => vote.UserId == userId && vote.ElectionId == electionId);
    }
    
    /// <inheritdoc/>
    public async Task<int> GetCandidateVotesByElectionIdAsync(Guid candidateId, Guid electionId)
    {
        return await _dbContext.Votes
            .CountAsync(vote => vote.CandidateId == candidateId && vote.ElectionId == electionId);
    }
}