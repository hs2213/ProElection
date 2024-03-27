using Microsoft.AspNetCore.Components;
using ProElection.Entities;
using ProElection.Entities.Enums;
using ProElection.Services.Interfaces;

namespace ProElection.Shared.Components;

public partial class SingleElection
{
    [Parameter] 
    public Election Election { get; set; } = default!;

    [Parameter]
    public User ViewingUser { get; set; } = default!;
    
    [Parameter]
    public EventCallback<(Election, UserType)> OnAddUserToElection { get; set; } = default!;
    
    [Inject]
    private NavigationManager _navigationManager { get; set; } = default!;
    
    [Inject]
    private IElectionService _electionService { get; set; } = default!;
    
    private string _electionCode = string.Empty;
    
    private void NavigateToVotePage()
    {
        _navigationManager.NavigateTo($"/vote/{Election.Id}");
    }
    
    private void AddUserToElection(UserType userType)
    {
        OnAddUserToElection.InvokeAsync((Election, userType));
    }

    private async Task GetElectionCode()
    {
        if ((ViewingUser.UserType == UserType.Voter && Election.End > DateTime.Now) == false)
        {
            return;
        }
        
        ElectionCode electionCodeResponse = 
            await _electionService.GetElectionCode(ViewingUser.Id, Election.Id);
        _electionCode = electionCodeResponse.Id.ToString();
    }
}