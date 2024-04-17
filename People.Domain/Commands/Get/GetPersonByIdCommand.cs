using MediatR;
using People.Domain.DTOs;

namespace People.Domain.Commands.Get;

public class GetPersonByIdCommand : IRequest<GenericCommandResult<ReadPersonDto?>>
{
    public int Id { get; private set; }
    public GetPersonByIdCommand(int id)
    {
        Id = id;
    }
    public GetPersonByIdCommand() { }
    public bool Valid()
    {
        return Id > 0;
    }
}
