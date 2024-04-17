using FluentValidation.Results;
using MediatR;
using People.Domain.DTOs;
using People.Domain.Entities;
using People.Domain.Validators.Person;

namespace People.Domain.Commands.Update;

public class UpdatePersonCommand : IRequest<GenericCommandResult<ReadPersonDto?>>
{
    public Person Person { get; private set; }
    public int Id { get; private set; }
    public UpdatePersonCommand(Person person, int id)
    {
        Person = person;
        Id = id;
    }
    public UpdatePersonCommand() { }
    public ValidationResult Valid()
    {
        CreatePersonValidator validator = new();
        return validator.Validate(Person);
    }
}
