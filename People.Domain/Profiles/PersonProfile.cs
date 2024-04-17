using AutoMapper;
using People.Domain.Commands.Create;
using People.Domain.Commands.Update;
using People.Domain.DTOs;
using People.Domain.Entities;
using People.Domain.ValueObjects;

namespace People.Domain.Profiles;
public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<CreatePersonDto, Person>();
        CreateMap<Person, CreatePersonCommand>();

        CreateMap<UpdatePersonCommand, Person>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.Person.CPF.Replace(".", "").Replace("-", "")))
            .ForMember(dest => dest.UF, opt => opt.MapFrom(src => src.Person.UF.ToString().ToUpper()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                new Name(src.Person.Name.FirstName.Trim().ToUpper(), src.Person.Name.LastName.Trim().ToUpper())
            ));

        CreateMap<UpdatePersonDto, Person>()
            .ForPath(dest => dest.Name.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForPath(dest => dest.Name.LastName, opt => opt.MapFrom(src => src.LastName));

        CreateMap<Person, ReadPersonDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Name.LastName));

        CreateMap<CreatePersonDto, CreatePersonCommand>()
           .ForMember(dest => dest.Person, opt => opt.MapFrom(src => new Person(
               new Name(src.FirstName, src.LastName),
               src.DateBirth.Date,
               src.UF,
               src.CPF))
           );

        CreateMap<UpdatePersonDto, UpdatePersonCommand>()
          .ForMember(dest => dest.Person, opt => opt.MapFrom(src => new Person(
              new Name(src.FirstName, src.LastName),
              src.DateBirth.Date,
              src.UF,
              src.CPF))
          );
    }
}
