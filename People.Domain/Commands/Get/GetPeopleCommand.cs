using MediatR;
using People.Domain.DTOs;

namespace People.Domain.Commands.Get;
public class GetPeopleCommand : IRequest<GenericCommandResult<IEnumerable<ReadPersonDto>?>> { }
