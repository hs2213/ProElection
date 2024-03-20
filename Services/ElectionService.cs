﻿using FluentValidation;
using ProElection.Entities;
using ProElection.Repositories.Interfaces;
using ProElection.Services.Interfaces;

namespace ProElection.Services;

public sealed class ElectionService : IElectionService
{
    private readonly IElectionRepository _electionRepository;
    private readonly IElectionCodeRepository _electionCodeRepository;
    private readonly IVoteRepository _voteRepository;
    private readonly IUserRepository _userRepository;
    
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
        IValidator<User> userValidator)
    {
        _electionRepository = electionRepository;
        _electionCodeRepository = electionCodeRepository;
        _voteRepository = voteRepository;
        _userRepository = userRepository;
        _electionCodeValidator = electionCodeValidator;
        _electionValidator = electionValidator;
        _voteValidator = voteValidator;
        _userValidator = userValidator;
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
        return await _electionCodeRepository.GetById(electionCodeId);
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
        await _voteValidator.ValidateAsync(vote);
        
        await _voteRepository.Create(vote);
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
        
        foreach (Guid candidateId in election.Candidates)
        {
            User? candidate = await _userRepository.GetUserById(candidateId);

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