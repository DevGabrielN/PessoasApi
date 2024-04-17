using MediatR;

namespace People.Domain.Commands.Autentication;

public class LoginUserCommand : IRequest<GenericCommandResult<string?>>
{
    public string Username { get; private set; }
    public string Password { get; private set; }
    public LoginUserCommand(string username, string password)
    {
        Username = username;
        Password = password;
    }
    public LoginUserCommand() { }
}
