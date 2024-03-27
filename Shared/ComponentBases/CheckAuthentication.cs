using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ProElection.Entities;
using ProElection.Services.Interfaces;

namespace ProElection.Shared.ComponentBases;

public class CheckAuthentication : ComponentBase
{
    [Inject]
    protected ProtectedSessionStorage ProtectedSessionStorage { get; set; } = null!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    protected IUserService UserService { get; set; } = null!;

    protected Guid UserId { get; set; }
    
    protected User? ViewingUser { get; set; }
    
    protected bool NavigateIfNotAuthenticated { get; set; } = true;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            ProtectedBrowserStorageResult<Guid> userIdResult = 
                await ProtectedSessionStorage.GetAsync<Guid>("userId");

            if (userIdResult.Success == false && NavigateIfNotAuthenticated)
            {
                NavigationManager.NavigateTo("/");
                return;
            }
            
            UserId = userIdResult.Value;
            await GetUser();
            
            StateHasChanged();
        }
        
        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task GetUser()
    {
        if (UserId == null)
        {
            return;
        }
            
        ViewingUser = await UserService.GetUserById(UserId);
    }
}