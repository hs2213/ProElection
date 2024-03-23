using Microsoft.AspNetCore.Components;
using ProElection.Entities;
using ProElection.Entities.Enums;
using ProElection.Entities.Validations;

namespace ProElection.Shared.Components;

public partial class AddUser
{
    [Parameter]
    public EventCallback<User> OnUserCreated { get; set; }

    [Parameter] 
    public string Label { get; set; } = string.Empty;

    [Parameter] 
    public string Class { get; set; } = string.Empty;
    
    [Parameter]
    public UserType UserType { get; set; }

    private ValidationContext _userContext = new ValidationContext();
    
    private User _user = GetEmptyEntity.User();

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        _user.UserType = UserType;
    }

    private async Task CreateUser()
    {
        _userContext.Reset();
        _userContext.ValidateEvent?.Invoke();

        if (_userContext.State == ValidationState.Invalid)
        {
            return;
        }

        await OnUserCreated.InvokeAsync(_user);
    }
}