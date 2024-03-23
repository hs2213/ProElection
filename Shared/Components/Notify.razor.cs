using Microsoft.AspNetCore.Components;
using ProElection.Services.Interfaces;

namespace ProElection.Shared.Components;

public partial class Notify
{
    /// <summary>
    /// Used to assign component to the service.
    /// </summary>
    [Inject] 
    private INotifyService _notifyService { get; set; } = default!;
    
    /// <summary>
    /// Message displayed on component
    /// </summary>
    private string _message = string.Empty;

    /// <summary>
    /// Determines whether the component should be rendered or not.
    /// </summary>
    private bool _showMessage = false;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        _notifyService.NotifyComponent = this;
    }

    /// <summary>
    /// Causes component to render an given message to be shown
    /// </summary>
    /// <param name="message"></param>
    public async Task NotifyMessage(string message)
    {
        _message = message;
        _showMessage = true;

        StateHasChanged();
        
        await Task.Delay(1500);

        _showMessage = false;
        
        StateHasChanged();
    }
}