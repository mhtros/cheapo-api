using System.Text.Json;
using Cheapo.Api.Entities;
using Cheapo.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cheapo.Api.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder AddDefaults(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("NOW()");

        modelBuilder.Entity<ApplicationTransaction>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("NOW()");

        return modelBuilder;
    }

    public static ModelBuilder AddConstraints(this ModelBuilder modelBuilder)
    {
        return modelBuilder;
    }

    public static void SeedData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationTransactionCategory>().HasData(GenerateTransactionCategoriesData()!);
    }

    private static IEnumerable<ApplicationTransactionCategory?> GenerateTransactionCategoriesData()
    {
        var json = File.ReadAllText("Data/Seed/transaction-categories.json");
        var categories = JsonSerializer.Deserialize<IEnumerable<ApplicationTransactionCategory>>(json);
        return categories ?? Array.Empty<ApplicationTransactionCategory>();
    }

    private static IEnumerable<IDistinctable<string>> AssignIds(IEnumerable<IDistinctable<string>> records)
    {
        foreach (var record in records)
        {
            record.Id = Guid.NewGuid().ToString();
            yield return record;
        }
    }
}