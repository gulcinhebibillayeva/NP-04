// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;

class CarContext : DbContext
{
    public DbSet<Car> Cars { get; set; }
    public CarContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=CarsDB;Integrated Security=True;Trust Server Certificate=True;");
    }
}


    
