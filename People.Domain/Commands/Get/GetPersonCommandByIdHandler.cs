using AutoMapper;
using FluentValidation.Results;
using MediatR;
using People.Domain.DTOs;
using People.Domain.Interfaces;

namespace People.Domain.Commands.Get;

public class GetPersonCommandByIdHandler : IRequestHandler<GetPersonByIdCommand, GenericCommandResult<ReadPersonDto?>>
{

    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    public GetPersonCommandByIdHandler(IPersonRepository personRepository, IMapper mapper = null)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    async Task<GenericCommandResult<ReadPersonDto?>> IRequestHandler<GetPersonByIdCommand, GenericCommandResult<ReadPersonDto?>>.Handle(GetPersonByIdCommand request, CancellationToken cancellationToken)
    {
        if (request.Valid())
        {
            var person = await _personRepository.FindByIdAsync(request.Id);
            if (person != null)
            {
                ReadPersonDto personDto = _mapper.Map<ReadPersonDto>(person);
                return new GenericCommandResult<ReadPersonDto?>(message: "Consulta realizada", data: personDto);
            }
            else
            {
                return new GenericCommandResult<ReadPersonDto?>(success: false, message: "Pessoa não encontrada");
            }
        }
        else
        {
            var errors = new List<ValidationFailure> { new("Person.Id", "Id inválido para consulta") };
            return new GenericCommandResult<ReadPersonDto?>(false, "Requisição inválida", null, errors);
        }
    }
}
