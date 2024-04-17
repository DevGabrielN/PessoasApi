using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using People.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace People.Domain.Commands.Autentication;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, GenericCommandResult<string?>>
{
    private readonly IMapper _mapper;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public LoginUserCommandHandler(IMapper mapper, SignInManager<User> signInManager, IConfiguration configuration = null)
    {
        _mapper = mapper;
        _signInManager = signInManager;
        _configuration = configuration;
    }
    public async Task<GenericCommandResult<string?>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);        
        if (!result.Succeeded)
        {
            var errorslist = new List<IdentityError> { new() };
           
            return new GenericCommandResult<string?>(success: false, message:"Falha ao realizar login", propertyName: "User", errorMessage: "Login ou senha inválidos");
        }
        var usuario = _signInManager
           .UserManager
           .Users
           .FirstOrDefault(user => user.NormalizedUserName == request.Username.ToUpper());

        return new GenericCommandResult<string?>(message: "Usuário criado", data: GenerateToken(usuario)); ;
    }

    public string GenerateToken(User usuario)
    {
        Claim[] claims =
        {
                new Claim("username", usuario.UserName ?? string.Empty),
                new Claim("id", usuario.Id),
                new Claim("date", DateTime.Now.ToString()),
        };
        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]));

        var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddMinutes(60),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
