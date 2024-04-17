using FluentValidation.Results;
using MediatR;
using People.Domain.DTOs;
using People.Domain.Validators.Person;

namespace People.Domain.Commands.Get;
public class GetPeopleByUFCommand : IRequest<GenericCommandResult<IEnumerable<ReadPersonDto>?>>
{
    public string UF { get; private set; }
    public GetPeopleByUFCommand(string uF)
    {
        UF = uF;
    }
    public ValidationResult Valid()
    {
        CreateUFValidator validator = new ();
        return validator.Validate(UF);        
    }
}
