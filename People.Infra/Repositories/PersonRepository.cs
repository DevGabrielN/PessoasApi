using Microsoft.EntityFrameworkCore;
using People.Data.Data;
using People.Domain.Entities;
using People.Domain.Interfaces;

namespace People.Infra.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly PeopleDBContext _context;
    public PersonRepository(PeopleDBContext context)
    {
        _context = context;
    }
    public async Task<Person?> AddPersonAsync(Person person)
    {
        if(await _context.People.Where(x => x.CPF == person.CPF).AnyAsync())
        {
            throw new DbUpdateException();
        }
        var result = await _context.People.AddAsync(person);        
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> DeletePersonAsync(int id)
    {
        var person = await FindByIdAsync(id);
        if (person != null)
        {
            _context.People.Remove(person);
            int affectedRows = await _context.SaveChangesAsync();
            return affectedRows > 0;
        }
        return false;
    }

    public async Task<Person?> FindByIdAsync(int id)
    {        
        return await _context.People.FindAsync(id);
    }

    public async Task<IEnumerable<Person?>> FindByUFAsync(string uf)
    {
        return await _context.People.Where(person => person.UF.ToUpper() == uf.ToUpper()).ToListAsync();
    }
    public async Task<IEnumerable<Person?>> GetAllAsync()
    {
        return await _context.People.ToListAsync();
    }
    public async Task<Person?> UpdatePersonAsync(Person person)
    {
        var existingPerson = await _context.People.FindAsync(person.Id);

        if (existingPerson != null)
        {
            _context.Entry(existingPerson).CurrentValues.SetValues(person);
            await _context.SaveChangesAsync();
        }
        return existingPerson;
    }
}
