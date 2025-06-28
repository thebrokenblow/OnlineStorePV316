using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Extensions;
using OnlineStore.Storage.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("OnlineStoreConnectionString") ?? 
    throw new ArgumentNullException("Не установленна строка подключения");

builder.Services
        .AddStorageDependency(connectionString)
        .AddApplicationDependency();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();