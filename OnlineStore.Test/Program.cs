using OnlineStore.Test.Dto;
using System.Net.Http.Json;

var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://localhost:7299/api/")
};

var responce = await httpClient.GetFromJsonAsync<List<GetAllProductCategoryVM>>("categories");

Console.ReadLine();