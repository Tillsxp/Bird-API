using Bird_API.Models;
using Microsoft.EntityFrameworkCore;
namespace Bird_API.Data;

public class BirdContext : DbContext
{
    public DbSet<Bird> Birds {get; set;}
    public BirdContext(DbContextOptions options) : base(options)
    {
    }
}
