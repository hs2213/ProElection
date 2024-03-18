using Microsoft.AspNetCore.Components;

namespace ProElection.Shared.Components;

public partial class CentredBoxWithTitle
{
    [Parameter] 
    public string Header { get; set; } = string.Empty;
    
    /// <summary>
    /// Allows HTML Elements and other components to be rendered inside this component.
    /// </summary>
    [Parameter] 
    public RenderFragment ChildContent { get; set; } = default!;
}