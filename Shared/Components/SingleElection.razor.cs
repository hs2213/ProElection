using Microsoft.AspNetCore.Components;
using ProElection.Entities;

namespace ProElection.Shared.Components;

public partial class SingleElection
{
    [Parameter] 
    public Election Election { get; set; } = default!;
    
    [Parameter]
    public bool IsAdmin { get; set; }
}