using ProElection.Entities;
using ProElection.Repositories.Interfaces;
using ProElection.Services.Interfaces;

namespace ProElection.Services;

public sealed class ElectionService : IElectionService
{
    private readonly IElectionRepository _electionRepository;
    private readonly IElectionCodeRepository _electionCodeRepository;
    private readonly IVoteRepository _voteRepository;
    private readonly IUserService _userService;

    public ElectionService(
        IElectionRepository electionRepository, 
        IElectionCodeRepository electionCodeRepository, 
        IVoteRepository voteRepository,
        IUserService userService)
    {
        _electionRepository = electionRepository;
        _electionCodeRepository = electionCodeRepository;
        _voteRepository = voteRepository;
        _userService = userService;
    }
    
    /// <inheritdoc/>
    public async Task<IEnumerable<Election>> GetAllElections()
    {
        return await _electionRepository.GetElections();
    }
    
    /// <inheritdoc/>
    public async Task<IEnumerable<Election>> GetElectionsByMultipleIds(IEnumerable<Guid> electionIds)
    {
        return await _electionRepository.GetMultipleElectionsByIds(electionIds);
    }

    public async Task<ElectionCode?> GetElectionCodeById(Guid electionCodeId)
    {
        return await _electionCodeRepository.Get(electionCodeId);
    }
    
    public async Task<ElectionCode> GetElectionCode(Guid electionId, Guid userId)
    {
        ElectionCode? electionCode = await _electionCodeRepository.GetByElectionAndUser(electionId, userId);

        if (electionCode != null)
        {
            return electionCode;
        }
        
        electionCode = new ElectionCode
        {
            ElectionId = electionId,
            UserId = userId,
            Id = Guid.NewGuid()
        };
        
        return await _electionCodeRepository.Create(electionCode);
    }
    
    public async Task Vote(Vote vote) => await _voteRepository.Create(vote);
    
    public async Task<bool> CheckIfUserVoted(Guid electionId, Guid userId) =>
        await _voteRepository.CheckIfUserVotedInElection(electionId, userId);

    public async Task<Dictionary<User, int>?> CalculateResults(Guid electionId)
    {
        Election? election = await _electionRepository.GetElectionById(electionId);

        if (election == null)
        {
            return null;
        }

        Dictionary<User, int> candidatesWithVoteCounts = await GetCandidatesWithVoteCounts(election);

        // Orders the candidates by the number of votes they got in ascending order
        return candidatesWithVoteCounts
            .OrderBy(candidateWithVoteCount => candidateWithVoteCount.Value)
            .ToDictionary(
                candidateWithVoteCount => candidateWithVoteCount.Key,
                candidateWithVoteCount => candidateWithVoteCount.Value);
    }

    
    /// <summary>
    /// Creates a dictionary where a key is a candidate and its value is the number of votes they got.
    /// </summary>
    /// <param name="election">Election to get vote count from</param>
    /// <returns><see cref="Dictionary{TKey,TValue}"/> where the key is a candidate
    /// and the value is the number of votes they got</returns>
    private async Task<Dictionary<User, int>> GetCandidatesWithVoteCounts(Election election)
    {
        Dictionary<User, int> candidatesWithVoteCount = new Dictionary<User, int>();
        
        foreach (Guid candidateId in election.Candidates)
        {
            User? candidate = await _userService.GetUserById(candidateId);

            if (candidate == null)
            {
                continue;
            }

            int noOfVotes = await _voteRepository.GetCandidateVotesByElectionId(candidateId, election.Id);
            
            candidatesWithVoteCount.Add(candidate, noOfVotes);
        }

        return candidatesWithVoteCount;
    }
}