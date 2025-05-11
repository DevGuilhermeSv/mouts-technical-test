using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.ModelBuilderConfigurations;

public static class EntitiesConfiguration
{
    public static void AddressConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>()
            .OwnsOne<Geolocation>(x => x.Geolocation)
            .Property(x => x.Lat).HasColumnName("Lat");
        
        modelBuilder.Entity<Address>()
            .OwnsOne<Geolocation>(x => x.Geolocation)
            .Property(x => x.Long).HasColumnName("Long");
    }
    public static void ProductConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .OwnsOne(x => x.Rating)
            .Property(x => x.Rate).HasColumnName("Rate");
        
        modelBuilder.Entity<Product>()
            .OwnsOne(x => x.Rating)
            .Property(x => x.Count).HasColumnName("Count");
    }
}