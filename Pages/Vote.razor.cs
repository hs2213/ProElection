using Microsoft.AspNetCore.Components;
using ProElection.Entities;
using ProElection.Entities.Enums;
using ProElection.Services.Interfaces;
using ProElection.Shared.ComponentBases;

namespace ProElection.Pages;

public partial class Vote : CheckAuthentication
{
    [Parameter]
    public Guid Id { get; set; }
    
    [Parameter]
    public bool IsInPerson { get; set; }
    
    [Inject]
    private IElectionService _electionService { get; set; } = default!;
    
    private Election? _election = default!;

    private ElectionCode? _electionCode;

    private Dictionary<User, int>? _candidatesResults;

    private List<User> _candidates = [];
    
    private bool _votingDisabled = false;
    
    private bool _alreadyVoted = false;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        NavigateIfNotAuthenticated = false;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            if (ViewingUser?.UserType is UserType.Candidate or UserType.Admin)
            {
                _votingDisabled = true;
            }

            if (IsInPerson)
            {
                await ProcessElectionCode();
            }
            else
            {
                await ProcessElectionId();
            }
            
            _candidates = await UserService.GetCandidatesForElection(_election!) as List<User> ?? [];
            
            if (_election!.End < DateTime.Now)
            {
                await GetResults();
                _votingDisabled = true;
            }

            _alreadyVoted = await _electionService.CheckIfUserVoted(_election.Id, UserId);
            
            StateHasChanged();
        }
    }

    private async Task ProcessElectionCode()
    {
        _electionCode = await _electionService.GetElectionCode(Id);

        if (_electionCode == null || _electionCode.Status == CodeStatus.Used)
        {
            NavigationManager.NavigateTo("/");
        }
        
        UserId = _electionCode!.UserId;
        ViewingUser = await UserService.GetUserById(UserId);
        
        _election = await _electionService.GetElectionById(_electionCode.ElectionId);
    }

    private async Task ProcessElectionId()
    {
        _election = await _electionService.GetElectionById(Id);

        if (_election == null)
        {
            NavigationManager.NavigateTo("/elections");
        }
    }
    
    private async Task GetResults()
    {
        _candidatesResults = await _electionService.CalculateResults(_election!.Id);
    }
    
    private async Task PlaceVote(User candidate)
    {
        if (_votingDisabled)
        {
            NavigationManager.NavigateTo("/elections");
            return;
        }
        
        Entities.Vote vote = new Entities.Vote
        {
            CandidateId = candidate.Id,
            ElectionId = _election!.Id,
            UserId = UserId,
            Id = Guid.NewGuid(),
            Time = DateTimeOffset.Now
        };
        
        await _electionService.Vote(vote);
        
        _votingDisabled = true;

        if (IsInPerson)
        {
            await _electionService.MarkElectionCodeAsUsed(_electionCode!);
        }
        
        NavigationManager.NavigateTo("/elections");
    }
}