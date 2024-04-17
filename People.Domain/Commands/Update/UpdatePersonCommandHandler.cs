using AutoMapper;
using MediatR;
using People.Domain.DTOs;
using People.Domain.Interfaces;

namespace People.Domain.Commands.Update;

public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, GenericCommandResult<ReadPersonDto?>>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public UpdatePersonCommandHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<GenericCommandResult<ReadPersonDto?>> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var result = request.Valid();
        var errors = result.Errors.ToList();
        if (!(request.Id > 0))
        {
            return new GenericCommandResult<ReadPersonDto?>(false, "Requisição inválida", null, propertyName: "Person.Id", errorMessage: "Id inválido para consulta");
        }
        var person = await _personRepository.FindByIdAsync(request.Id);

        if (person != null)
        {
            try
            {
                if (result.IsValid)
                {
                    _mapper.Map(request, person);
                    var updatePerson = await _personRepository.UpdatePersonAsync(person);
                    var readPersonDto = _mapper.Map<ReadPersonDto>(updatePerson);
                    return new GenericCommandResult<ReadPersonDto?>(message: "Update concluído", data: readPersonDto);
                }
                return new GenericCommandResult<ReadPersonDto?>(false, "Campo(s) não validado(s)", null, errors);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult<ReadPersonDto?>(success: false, message: $"Ocorreu um erro inesperado:", errorMessage: $"{ex.Message}");
            }
        }
        else
        {
            return new GenericCommandResult<ReadPersonDto?>(success: false, message: "Pessoa não encontrada");

        }
    }
}
