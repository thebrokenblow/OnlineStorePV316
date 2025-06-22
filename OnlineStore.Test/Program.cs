using OnlineStore.Test;
using OnlineStore.Test.Dto;
using OnlineStore.Test.ViewModel;
using System.Net;
using System.Net.Http.Json;

var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://localhost:7299/api/")
};

var productDto = new ProductDto
{
    Name = "Iphone 16",
    Description = "Apple",
    Price = 100_000,
    ProductCategoryId = 1
};

var responce = await httpClient.PostAsJsonAsync("Product", productDto);

if (responce.StatusCode == HttpStatusCode.BadRequest)
{
    var productDetailsVM = await responce.Content.ReadFromJsonAsync<ValidateResult>();
}