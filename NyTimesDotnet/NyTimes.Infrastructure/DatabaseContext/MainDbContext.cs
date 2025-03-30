using Microsoft.EntityFrameworkCore;
using NyTimes.Domain.Models;

namespace NyTimes.Infrastructure.DatabaseContext
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

        public DbSet<Articles> Articles { get; set; }
        public DbSet<Facets> Facets { get; set; }
        public DbSet<Multimedia> Multimedia { get; set; }
    }
}