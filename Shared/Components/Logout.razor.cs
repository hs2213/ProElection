using ProElection.Shared.ComponentBases;

namespace ProElection.Shared.Components;

public partial class Logout : CheckAuthentication
{
    private async Task LogOut()
    {
        await ProtectedSessionStorage.DeleteAsync("userId");
        NavigationManager.NavigateTo("/");
        StateHasChanged();
    }
}