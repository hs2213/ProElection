using Microsoft.AspNetCore.Components;
using ProElection.Entities;
using ProElection.Entities.Enums;
using ProElection.Services.Interfaces;
using ProElection.Shared.ComponentBases;

namespace ProElection.Pages;

public partial class ElectionsView : CheckAuthentication
{
    private List<Election> _elections = [];
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _elections = await UserService.GetUserElections(UserId) as List<Election> ?? [];
            
            StateHasChanged();
        }
    }

    private bool CheckIfUserIsCandidate()
    {
        return ViewingUser?.UserType == UserType.Candidate;
    }
}