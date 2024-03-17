using FluentValidation;
using ProElection.Entities.Enums;

namespace ProElection.Entities.Validations;

public class ElectionCodeValidator : UserElectionAssociationValidator<ElectionCode>
{
    public ElectionCodeValidator()
    {
        RuleFor(electionCode => electionCode.Status)
            .NotNull().WithMessage("Status is required")
            .IsInEnum().WithMessage("Enum value is not valid");
    }
}