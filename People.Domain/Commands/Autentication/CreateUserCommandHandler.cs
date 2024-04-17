using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using People.Domain.DTOs;
using People.Domain.Entities;

namespace People.Domain.Commands.Autentication;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, GenericCommandResult<ReadUserDto?>>
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    public CreateUserCommandHandler(IMapper mapper, UserManager<User> userManager)
    {
        _mapper = mapper; 
        _userManager = userManager;
    }

    public async Task<GenericCommandResult<ReadUserDto?>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return new GenericCommandResult<ReadUserDto?>(false, "Falha ao cadastrar usuário", null, null, result.Errors.ToList());
        }
        var userDto = _mapper.Map<ReadUserDto>(user);
        return new GenericCommandResult<ReadUserDto?>(message: "Usuário criado", data: userDto);
    }
}
