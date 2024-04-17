using People.Domain.Entities;

namespace People.Domain.Interfaces;

public interface IPersonRepository
{
    Task<Person?> AddPersonAsync(Person person);    
    Task<IEnumerable<Person?>> GetAllAsync();
    Task<Person?> FindByIdAsync(int id);
    Task<IEnumerable<Person?>> FindByUFAsync(string uf);
    Task<bool> DeletePersonAsync(int id);
    Task<Person?> UpdatePersonAsync(Person person);
}
