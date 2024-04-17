using MediatR;
using People.Domain.DTOs;

namespace People.Domain.Commands.Autentication;

public class CreateUserCommand : IRequest<GenericCommandResult<ReadUserDto?>>
{
    public string Username { get; private set; }
    public string Password { get; private set; }
    public CreateUserCommand(string username, string password)
    {
        Username = username;
        Password = password;
    }
    public CreateUserCommand() { }
}
