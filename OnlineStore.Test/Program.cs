using OnlineStore.Test;
using System.Net.Http.Json;

var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://localhost:7299/api/")
};

var productCategory = new ProductCategory
{
    Name = "Телефон",
    Description = "Крутые телефоны"
};

var responce = await httpClient.PostAsJsonAsync("ProductCategory", productCategory);

Console.ReadLine();