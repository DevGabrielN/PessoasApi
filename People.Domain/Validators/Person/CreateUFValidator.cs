using FluentValidation;
using People.Domain.Erros.Person;

namespace People.Domain.Validators.Person;
public class CreateUFValidator : AbstractValidator<string>
{
    public CreateUFValidator()
    {
        RuleFor(uf => uf)
            .NotEmpty().WithMessage(CreatePersonErrors.UFRequired)
            .Matches(@"^(?i)(\s*(AC|AL|AP|AM|BA|CE|DF|ES|GO|MA|MT|MS|MG|PA|PB|PR|PE|PI|RJ|RN|RS|RO|RR|SC|SP|SE|TO)?)$")
            .WithMessage(CreatePersonErrors.InvalidUF)
            .OverridePropertyName("UF");
    }
}
