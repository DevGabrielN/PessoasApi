using Microsoft.EntityFrameworkCore;
using People.Domain.Entities;

namespace People.Data.Data;
public class PeopleDBContext : DbContext
{
    public PeopleDBContext(DbContextOptions<PeopleDBContext> opts) : base(opts) { }
    public DbSet<Person> People { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Person>(person =>
        {
            person.OwnsOne(p => p.Name);            
            person.HasIndex(p => p.CPF).IsUnique();
        });
    }
}
