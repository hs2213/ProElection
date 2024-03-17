using ProElection.Entities;
using ProElection.Repositories.Interfaces;
using ProElection.Services.Interfaces;

namespace ProElection.Services;

public sealed class ElectionService : IElectionService
{
    private readonly IElectionRepository _electionRepository;
    private readonly IElectionCodeRepository _electionCodeRepository;
    private readonly IVoteRepository _voteRepository;

    public ElectionService(
        IElectionRepository electionRepository, 
        IElectionCodeRepository electionCodeRepository, 
        IVoteRepository voteRepository)
    {
        _electionRepository = electionRepository;
        _electionCodeRepository = electionCodeRepository;
        _voteRepository = voteRepository;
    }
    
    public async Task<IEnumerable<Election>> GetAllElections()
    {
        return await _electionRepository.GetElections();
    }
    
}