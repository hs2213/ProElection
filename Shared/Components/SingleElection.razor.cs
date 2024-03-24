using Microsoft.AspNetCore.Components;
using ProElection.Entities;
using ProElection.Entities.Enums;

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
    
    private void NavigateToVotePage()
    {
        _navigationManager.NavigateTo($"/vote/{Election.Id}");
    }
    
    private void AddUserToElection(UserType userType)
    {
        OnAddUserToElection.InvokeAsync((Election, userType));
    }
}