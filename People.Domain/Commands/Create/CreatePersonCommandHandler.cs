using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Domain.DTOs;
using People.Domain.Interfaces;

namespace People.Domain.Commands.Create;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, GenericCommandResult<ReadPersonDto?>>
{    
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    public CreatePersonCommandHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    async Task<GenericCommandResult<ReadPersonDto?>> IRequestHandler<CreatePersonCommand, GenericCommandResult<ReadPersonDto?>>.Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var result =  request.Valid();
        var errors = result.Errors.ToList();
        if (result.IsValid)
        {
            try
            {
                var person = await _personRepository.AddPersonAsync(request.Person);
                var personDto = _mapper.Map<ReadPersonDto>(person);
                return new GenericCommandResult<ReadPersonDto?>(message: "Criado com sucesso", data: personDto);
            }
            catch (Exception e)
            when (e is DbUpdateException)
            {
                var errorslist = new List<ValidationFailure>{new("Person.CPF", "Já existe um cadastro para o CPF informado")};

                return new GenericCommandResult<ReadPersonDto?>(false, "Falha ao registrar pessoa", null, errorslist);
            }
        }
        else
        {
            return new GenericCommandResult<ReadPersonDto?>(false, "Campo(s) não validado(s)",null, errors);
        }
    }
}