using AutoMapper;
using MediatR;
using People.Domain.DTOs;
using People.Domain.Interfaces;

namespace People.Domain.Commands.Get;

public class GetPeopleByUFCommandHandler : IRequestHandler<GetPeopleByUFCommand, GenericCommandResult<IEnumerable<ReadPersonDto>?>>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetPeopleByUFCommandHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<GenericCommandResult<IEnumerable<ReadPersonDto>?>> Handle(GetPeopleByUFCommand request, CancellationToken cancellationToken)
    {
        var result = request.Valid();
        var errors = result.Errors.ToList();
        if (result.IsValid)
        {
            try
            {
                var people = await _personRepository.FindByUFAsync(request.UF);
                if (people != null && people.Any())
                {
                    IEnumerable<ReadPersonDto>? peopleDto = _mapper.Map<IEnumerable<ReadPersonDto>>(people);
                    return new GenericCommandResult<IEnumerable<ReadPersonDto>?>(message: "Consulta realizada", data: peopleDto);
                }

                return new GenericCommandResult<IEnumerable<ReadPersonDto>?>(success: false, message: "Não foi encontrado nenhum resultado");
            }
            catch (Exception ex)
            {
                return new GenericCommandResult<IEnumerable<ReadPersonDto>?>(success: false, message: $"Ocorreu um erro inesperado", errorMessage: $"{ex.Message}");
            }
        }
        else
        {
            return new GenericCommandResult<IEnumerable<ReadPersonDto>?>(false, "Campo(s) não validado(s)", null, errors);
        }
    }
}
