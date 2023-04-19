
using System.Text;
using System.Text.Json;
using ProductService.Dtos;

namespace ProductServices.SyncDataServices.Http
{
    public class OrderDataClient : IOrderDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public OrderDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendProductToOrder(ReadProductDto readProductDto)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(readProductDto), Encoding.UTF8,"application/json");
            var response = await _httpClient.PostAsync($"{_configuration["OrderServices"]}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync Post to Order Services was Ok");
            }
            else
            {
                Console.WriteLine("--> Sync Post to Order Services was Not Ok");
            }
        }
    }
}
