using Microsoft.AspNetCore.Components;
using ProElection.Entities.Enums;
using ProElection.Entities.Validations;

namespace ProElection.Shared.Components;

public partial class ValidatedInput<TEntity> where TEntity : class 
{
    /// <summary>
    /// Specifies the name of the label being entered to the user.
    /// </summary>
    [Parameter] 
    public string LabelName { get; set; } = string.Empty;
    
    /// <summary>
    /// Specifies the type of input to be used by the HTML input tag.
    /// </summary>
    [Parameter]
    public InputType InputType { get; set; }
    
    /// <summary>
    /// Used to attach the validation to the context by assigning its events to this validation.
    /// </summary>
    [Parameter]
    public ValidationContext ValidationContext { get; set; }
    
    /// <summary>
    /// Specifies the class being 
    /// </summary>
    [Parameter]
    public TEntity Entity { get; set; }
}