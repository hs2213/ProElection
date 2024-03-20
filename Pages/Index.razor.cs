using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ProElection.Entities;
using ProElection.Entities.Enums;
using ProElection.Entities.Validations;
using ProElection.Services.Interfaces;

namespace ProElection.Pages;

public partial class Index
{
    [Inject] 
    private NavigationManager _navigationManager { get; set; } = default!;

    [Inject] 
    private IUserService _userService { get; set; } = default!;

    [Inject] 
    private ProtectedSessionStorage _protectedSessionStorage { get; set; } = default!;

    private bool _signUpEnabled = false;

    private ValidationContext _userContext = new ValidationContext();

    private User _user = GetEmptyEntity.User();

    private async Task AttemptSignIn()
    {
        if (ValidateUser() == ValidationState.Invalid)
        {
            return;
        }

        User? retrievedUser = await _userService.Authenticate(_user.Email, _user.HashedPassword);

        // If email and details are incorrect
        if (retrievedUser == null)
        {
            return;
        }

        await _protectedSessionStorage.SetAsync("userId", retrievedUser.Id);
        
        _navigationManager.NavigateTo("/elections");
    }
    
    private ValidationState ValidateUser()
    {
        _userContext.Reset();
        _userContext.ValidateEvent?.Invoke();

        return _userContext.State;
    }

}