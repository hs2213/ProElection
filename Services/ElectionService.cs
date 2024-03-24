using FluentValidation;
using ProElection.Entities;
using ProElection.Entities.Enums;
using ProElection.Repositories.Interfaces;
using ProElection.Services.Interfaces;

namespace ProElection.Services;

public sealed class ElectionService : IElectionService
{
    private readonly IElectionRepository _electionRepository;
    private readonly IElectionCodeRepository _electionCodeRepository;
    private readonly IVoteRepository _voteRepository;
    private readonly IUserRepository _userRepository;
    
    private readonly INotifyService _notifyService;
    
    private readonly IValidator<ElectionCode> _electionCodeValidator;
    private readonly IValidator<Election> _electionValidator;
    private readonly IValidator<Vote> _voteValidator;
    private readonly IValidator<User> _userValidator;

    public ElectionService(
        IElectionRepository electionRepository, 
        IElectionCodeRepository electionCodeRepository, 
        IVoteRepository voteRepository,
        IUserRepository userRepository, 
        IValidator<ElectionCode> electionCodeValidator, 
        IValidator<Election> electionValidator,
        IValidator<Vote> voteValidator,
        IValidator<User> userValidator, 
        INotifyService notifyService)
    {
        _electionRepository = electionRepository;
        _electionCodeRepository = electionCodeRepository;
        _voteRepository = voteRepository;
        _userRepository = userRepository;
        _electionCodeValidator = electionCodeValidator;
        _electionValidator = electionValidator;
        _voteValidator = voteValidator;
        _userValidator = userValidator;
        _notifyService = notifyService;
    }
    
    /// <inheritdoc/>
    public async Task<Election> CreateElection(Election election)
    {
        await _electionValidator.ValidateAndThrowAsync(election);
        Election electionCreated = await _electionRepository.CreateElection(election);
        await _notifyService.ShowNotification("Election Created");
        return electionCreated;
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

    /// <inheritdoc/>
    public async Task<ElectionCode?> GetElectionCode(Guid electionCodeId)
    {
        ElectionCode? electionCode = await _electionCodeRepository.GetById(electionCodeId);

        if (electionCode == null)
        {
            await _notifyService.ShowNotification("Failed to get election from code. Please try again");
        }

        if (electionCode!.Status == CodeStatus.Used)
        {
            await _notifyService.ShowNotification("Code has already been used.");
        }

        return electionCode;
    }
    
    /// <inheritdoc/>
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

        await _electionCodeValidator.ValidateAndThrowAsync(electionCode);
        
        return await _electionCodeRepository.Create(electionCode);
    }

    /// <inheritdoc/>
    public async Task Vote(Vote vote)
    {
        await _voteValidator.ValidateAndThrowAsync(vote);
        
        await _voteRepository.Create(vote);

        await _notifyService.ShowNotification("Vote Sent");
    } 
    
    /// <inheritdoc/>
    public async Task<bool> CheckIfUserVoted(Guid electionId, Guid userId) =>
        await _voteRepository.CheckIfUserVotedInElection(electionId, userId);

    /// <inheritdoc/>
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
        
        IEnumerable<User> candidates = await _userRepository.GetCandidatesOfAnElection(election.Id);
        
        foreach (User candidate in candidates)
        {
            int noOfVotes = await _voteRepository.GetCandidateVotesByElectionId(candidate.Id, election.Id);
            
            candidatesWithVoteCount.Add(candidate, noOfVotes);
        }

        return candidatesWithVoteCount;
    }
}