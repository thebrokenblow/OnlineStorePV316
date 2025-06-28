
using OnlineStore.Test.Dto;
using System.Net;
using System.Net.Http.Json;

var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://localhost:7299/api/")
};

var productDto = new ProductDto
{
    Name = "Some text",
    Description = "Some text",
    Price = 1000,
    ProductCategoryId = 1
};

var responce = await httpClient.PostAsJsonAsync("products", productDto);

if (responce.StatusCode != HttpStatusCode.Created)
{
    var errorResponse = await responce.Content.ReadAsStringAsync();
    Console.WriteLine(errorResponse);
}
Console.ReadLine();