namespace PARCIAL1A.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

using Microsoft.Identity.Client;

public class parcial1AContext: DbContext
    {
    public parcial1AContext(DbContextOptions<parcial1AContext> options) : base(options)
    {
    }
    public DbSet <Autores> Autores { get; set; }
    public DbSet<AutoresLibro> AutoresLibro { get; set; }
    public DbSet<Posts> Posts { get; set; }
    public DbSet<Libros> libros { get; set; }

}
    

