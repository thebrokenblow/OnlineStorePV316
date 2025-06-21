using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Data.Repositories;
using OnlineStore.Data.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("OnlineStoreConnectionString");
builder.Services.AddDbContext<OnlineStoreDBContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IRepositoryProductCategory, RepositoryProductCategory>();
builder.Services.AddScoped<IRepositoryProduct, RepositoryProduct>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();