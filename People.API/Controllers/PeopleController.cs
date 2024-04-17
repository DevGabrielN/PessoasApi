using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using People.Domain.Commands.Create;
using People.Domain.Commands.Delete;
using People.Domain.Commands.Get;
using People.Domain.Commands.Update;
using People.Domain.DTOs;
using People.Domain.Entities;

namespace People.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]

public class PeopleController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public PeopleController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] CreatePersonDto personDto)
    {
        var command = _mapper.Map<CreatePersonCommand>(personDto);
        var result = await _mediator.Send(command);

        return result.Data != null ?
            CreatedAtAction(nameof(GetPersonById), new { id = result.Data.Id }, result) :
            BadRequest(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetPeople()
    {
        var result = await _mediator.Send(new GetPeopleCommand());
        return result.Data != null ? Ok(result) : NotFound(result);
    }

    [HttpGet("GetById/{Id}")]
    public async Task<IActionResult> GetPersonById(int Id)
    {
        var command = new GetPersonByIdCommand(Id);
        var result = await _mediator.Send(command);

        return result.Data != null ? Ok(result) : result.Errors != null && result.Errors.Any() ? BadRequest(result) : NotFound(result);
    }

    [HttpGet("GetByUF/{UF}")]
    public async Task<IActionResult> GetPeopleByUf(string UF)
    {
        var result = await _mediator.Send(new GetPeopleByUFCommand(UF));

        return result.Data != null ? Ok(result) : result.Errors != null && result.Errors.Any() ? BadRequest(result) : NotFound(result);
    }

    [HttpDelete("DeleteById/{Id}")]
    public async Task<IActionResult> DeleteById(int Id)
    {
        var result = await _mediator.Send(new DeletePersonByIdCommand(Id));

        return result.Success == true ? Ok(result) : result.Errors != null && result.Errors.Any() ? BadRequest(result) : NotFound(result);
    }
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdatePerson(int Id, [FromBody] UpdatePersonDto personDto)
    {
        var person = _mapper.Map<Person>(personDto);
        var command = new UpdatePersonCommand(person, Id);
        var result = await _mediator.Send(command);

        return result.Data != null ? Ok(result) : result.Errors != null && result.Errors.Any() ? BadRequest(result) : NotFound(result);
    }
}