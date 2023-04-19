using ProductService.Dtos;

namespace ProductServices.SyncDataServices.Http
{
    public interface IOrderDataClient
    {
        Task SendProductToOrder(ReadProductDto readProductDto);
    }
}
