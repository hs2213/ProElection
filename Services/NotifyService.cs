using ProElection.Services.Interfaces;
using ProElection.Shared.Components;

namespace ProElection.Services;

public class NotifyService : INotifyService
{
    public Notify? NotifyComponent { get; set; }

    public async Task ShowNotification(string message)
    {
        if (NotifyComponent == null)
        {
            return;
        }
        
        await NotifyComponent.NotifyMessage(message);
    }
}