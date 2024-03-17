using ProElection.Entities.Enums;

namespace ProElection.Entities.Validations;

public class ValidationContext
{
    public Action? Reset { get; set; }

    public Action? Validate { get; set; }

    public ValidationState State { get; set; } = ValidationState.Valid;
}