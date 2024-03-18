using Microsoft.AspNetCore.Components;

namespace ProElection.Pages;

public partial class Index
{
    [Inject] 
    private NavigationManager _navigationManager { get; set; } = default!;
}