using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain;

namespace OnlineStore.Storage.Data;

public class OnlineStoreDBContext(DbContextOptions<OnlineStoreDBContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
}