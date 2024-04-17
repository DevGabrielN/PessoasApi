using FluentValidation.Results;
using MediatR;
using People.Domain.DTOs;
using People.Domain.Entities;
using People.Domain.Validators.Person;

namespace People.Domain.Commands.Create;
public class CreatePersonCommand : IRequest<GenericCommandResult<ReadPersonDto?>>
{
    public Person Person { get; private set; }    
    public CreatePersonCommand(Person person)
    {
        Person = person;            
    }
    public CreatePersonCommand() { }
    public ValidationResult Valid()
    {
       CreatePersonValidator validator = new();
       return validator.Validate(Person);
    }
}
