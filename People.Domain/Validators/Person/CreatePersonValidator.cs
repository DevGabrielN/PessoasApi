using DocumentValidator;
using FluentValidation;
using People.Domain.Erros.Person;

namespace People.Domain.Validators.Person;

public class CreatePersonValidator : AbstractValidator<Entities.Person>
{
    public CreatePersonValidator()
    {
        RuleFor(person => person.Name)
            .NotEmpty().NotNull().WithMessage(CreatePersonErrors.FirstNameRequired);

        RuleFor(person => person.Name.FirstName)
           .NotEmpty().WithMessage(CreatePersonErrors.FirstNameRequired)
           .Length(3, 150).WithMessage(CreatePersonErrors.FirstNameLengthRequirement)           
           .Matches(@"^[A-Za-zÀ-úçÇ']+(\s[A-Za-zÀ-úçÇ']+)*$").WithMessage(CreatePersonErrors.InvalidFirstName);

        RuleFor(person => person.Name.LastName)
            .NotEmpty().WithMessage(CreatePersonErrors.LastNameRequired)
            .Length(3, 150).WithMessage(CreatePersonErrors.LastNameLengthRequirement)
            .Matches(@"^[A-Za-zÀ-úçÇ']+(\s[A-Za-zÀ-úçÇ']+)*$").WithMessage(CreatePersonErrors.InvalidLastName);

        RuleFor(person => person.DateBirth.Date)
            .NotEmpty().NotNull().WithMessage(CreatePersonErrors.DateBirthRequired)
            .LessThanOrEqualTo(DateTime.Now.Date).WithMessage(CreatePersonErrors.DateBirthMustBeLessOrEqualToNow)
            .GreaterThanOrEqualTo(DateTime.Now.AddYears(-150).Date).WithMessage(CreatePersonErrors.DateBirthMustBeWithin150Years);

        RuleFor(person => person.CPF)
            .NotEmpty().WithMessage(CreatePersonErrors.CPFRequired)
            .Must(x => CpfValidation.Validate(x)).WithMessage(CreatePersonErrors.InvalidCPF);

        RuleFor(person => person.UF)
            .SetValidator(new CreateUFValidator())
            .OverridePropertyName("");
    }
}
