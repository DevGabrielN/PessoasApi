using FluentValidation.Results;
using MediatR;
using People.Domain.Interfaces;

namespace People.Domain.Commands.Delete;

public class DeletePersonByIdCommandHandler : IRequestHandler<DeletePersonByIdCommand, GenericCommandResult<object?>>
{
    private readonly IPersonRepository _personRepository;   

    public DeletePersonByIdCommandHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<GenericCommandResult<object?>> Handle(DeletePersonByIdCommand request, CancellationToken cancellationToken)
    {
        if (request.Valid())
        {
            var person = await _personRepository.DeletePersonAsync(request.Id);
            if (person)
            {                
                return new GenericCommandResult<object?>(message: "Delete concluído");
            }
            else
            {
                return new GenericCommandResult<object?>(success: false, message: "Pessoa não encontrada");
            }
        }
        else
        {
            var errors = new List<ValidationFailure> { new("Person.Id", "Id inválido para delete") };
            return new GenericCommandResult<object?>(false, "Requisição inválida", null, errors);
        }
    }
}
