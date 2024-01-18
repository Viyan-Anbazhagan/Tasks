using LoginAPI.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext:DbContext{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options){

    }
    public DbSet<UserCredential>? UserCredentials { get; set; }
}
