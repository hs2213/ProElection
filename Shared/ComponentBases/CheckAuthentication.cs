using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace ProElection.Shared.ComponentBases;

public class CheckAuthentication : ComponentBase
{
    [Inject]
    private ProtectedSessionStorage _protectedSessionStorage { get; set; }
    
    [Inject]
    private NavigationManager _navigationManager { get; set; }
    
    protected Guid UserId { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            ProtectedBrowserStorageResult<Guid> userIdResult = 
                await _protectedSessionStorage.GetAsync<Guid>("userId");

            if (userIdResult.Success == false)
            {
                _navigationManager.NavigateTo("/");
                return;
            }
            
            UserId = userIdResult.Value;
        }
        
        await base.OnAfterRenderAsync(firstRender);
    }
}