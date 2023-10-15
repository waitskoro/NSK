using Microsoft.EntityFrameworkCore;

namespace WebAPI.Model;

public class ApplicationContext : DbContext
{
    public DbSet<Person> persons { get; set; }
    public DbSet<Skills> skills { get; set; }

    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

}