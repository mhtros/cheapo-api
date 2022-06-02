using Cheapo.Api.Entities;
using Cheapo.Api.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cheapo.Api.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ApplicationInternalError> ApplicationInternalErrors { get; set; } = null!;

    public DbSet<ApplicationTransactionCategory> ApplicationTransactionCategories { get; set; } = null!;

    /// Οn model creating add restrictions, seed data, primary keys or default values.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddConstraints().AddDefaults().SeedData();
    }
}