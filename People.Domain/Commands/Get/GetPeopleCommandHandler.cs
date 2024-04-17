using AutoMapper;
using MediatR;
using People.Domain.DTOs;
using People.Domain.Interfaces;

namespace People.Domain.Commands.Get;

public class GetPeopleCommandHandler : IRequestHandler<GetPeopleCommand, GenericCommandResult<IEnumerable<ReadPersonDto>?>>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetPeopleCommandHandler(IPersonRepository personRepository, IMapper mapper = null)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<GenericCommandResult<IEnumerable<ReadPersonDto>?>> Handle(GetPeopleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var people = await _personRepository.GetAllAsync();
            if(people != null && people.Any()) 
            {
                IEnumerable<ReadPersonDto>? peopleDto = _mapper.Map<IEnumerable<ReadPersonDto>>(people);
                return new GenericCommandResult<IEnumerable<ReadPersonDto>?>(message: "Consulta realizada", data: peopleDto);
            }

            return new GenericCommandResult<IEnumerable<ReadPersonDto>?>(success:false, message: "Não foi encontrado nenhum resultado");
        }
        catch (Exception ex)
        {
            return new GenericCommandResult<IEnumerable<ReadPersonDto>?>(success:false, message: $"Ocorreu um erro inesperado:" , errorMessage: $"{ex.Message}");
        }
    }
}
