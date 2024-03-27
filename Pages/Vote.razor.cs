using Microsoft.AspNetCore.Components;
using ProElection.Entities;
using ProElection.Entities.Enums;
using ProElection.Services.Interfaces;
using ProElection.Shared.ComponentBases;

namespace ProElection.Pages;

public partial class Vote : CheckAuthentication
{
    [Parameter]
    public Guid ElectionId { get; set; }
    
    [Inject]
    private IElectionService _electionService { get; set; } = default!;
    
    private Election? _election = default!;

    private Dictionary<User, int>? _candidatesResults;

    private List<User> _candidates = [];
    
    private bool _votingDisabled = false;
    
    private bool _alreadyVoted = false;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            if (ViewingUser?.UserType is UserType.Candidate or UserType.Admin)
            {
                _votingDisabled = true;
            }
            
            _election = await _electionService.GetElectionById(ElectionId);

            if (_election == null)
            {
                NavigationManager.NavigateTo("/elections");
            }

            _candidates = await UserService.GetCandidatesForElection(_election!) as List<User> ?? [];
            
            if (_election!.End < DateTime.Now)
            {
                await GetResults();
                _votingDisabled = true;
            }

            _alreadyVoted = await _electionService.CheckIfUserVoted(_election.Id, UserId);
        }
    }
    
    private async Task GetResults()
    {
        _candidatesResults = await _electionService.CalculateResults(ElectionId);
    }
    
    private async Task PlaceVote(User candidate)
    {
        if (_votingDisabled)
        {
            return;
        }
        
        Entities.Vote vote = new Entities.Vote
        {
            CandidateId = candidate.Id,
            ElectionId = ElectionId,
            UserId = UserId,
            Id = Guid.NewGuid(),
            Time = DateTimeOffset.Now
        };
        
        await _electionService.Vote(vote);
        
        _votingDisabled = true;
    }
}