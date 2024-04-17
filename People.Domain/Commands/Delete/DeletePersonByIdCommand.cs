using MediatR;

namespace People.Domain.Commands.Delete;

public class DeletePersonByIdCommand : IRequest<GenericCommandResult<object?>>
{
    public int Id { get; private set; }
    public DeletePersonByIdCommand(int id)
    {
        Id = id;
    }
    public DeletePersonByIdCommand() { }
    public bool Valid()
    {
        return Id > 0;
    }
}
