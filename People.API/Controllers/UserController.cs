using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using People.Domain.Commands.Autentication;
using People.Domain.DTOs;

namespace People.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
    {
        var command = _mapper.Map<CreateUserCommand>(createUserDto);
        var result = await _mediator.Send(command);

        return result.Data != null ? StatusCode(201, result) : BadRequest(result);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginUserDto createUserDto)
    {
        var command = _mapper.Map<LoginUserCommand>(createUserDto);
        var result = await _mediator.Send(command);

        return result.Data != null ? Ok(result) : BadRequest(result);
    }
}
