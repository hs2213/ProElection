﻿using ProElection.Entities;

namespace ProElection.Repositories.Interfaces;

/// <summary>
/// Repository containing methods for interacting with the votes table in the database.
/// </summary>
public interface IVoteRepository
{
    /// <summary>
    /// Creates a new vote in the votes table in the database.
    /// </summary>
    /// <param name="vote"><see cref="Vote"/> to add to the database.</param>
    /// <returns></returns>
    public Task Create(Vote vote);

    /// <summary>
    /// Checks if a user has voted in a specific election.
    /// </summary>
    /// <param name="userId"><see cref="Guid"/> ID of the user to check.</param>
    /// <param name="electionId"><see cref="Guid"/> ID of the election to check.</param>
    /// <returns>true if the user has voted in the given election</returns>
    public Task<bool> CheckIfUserVotedInElection(Guid userId, Guid electionId);

    /// <summary>
    /// Gets the number of votes a candidate has in a specific election.
    /// </summary>
    /// <param name="candidateId"><see cref="Guid"/> ID of the candidate to check.</param>
    /// <param name="electionId"><see cref="Guid"/> ID of the election to check.</param>
    /// <returns>Number of votes the candidate has in the given election.</returns>
    public Task<int> GetCandidateVotesByElectionId(Guid candidateId, Guid electionId);
}