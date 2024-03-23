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

    private readonly ValidationContext _userContext = new ValidationContext();

    private readonly User _user = GetEmptyEntity.User();

    private async Task AttemptSignIn()
    {
        _user.UserType = UserType.Voter;
        
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

        await SetUserAndNavigate(retrievedUser);
    }
    
    private ValidationState ValidateUser()
    {
        _userContext.Reset();
        _userContext.ValidateEvent?.Invoke();

        return _userContext.State;
    }

    private async Task CreateUser(User userToCreate)
    {
        userToCreate.UserType = UserType.Voter;

        User? createdUser = await _userService.CreateUser(userToCreate);

        // if user could not be created
        if (createdUser == null )
        {
            return;
        }

        await SetUserAndNavigate(createdUser);
    }

    private async Task SetUserAndNavigate(User user)
    {
        await _protectedSessionStorage.SetAsync("userId", user.Id);

        if (user.UserType == UserType.Admin)
        {
            _navigationManager.NavigateTo("/admin");
            return;
        }
        
        _navigationManager.NavigateTo("/elections");
    }
}