using BigShoeCopmany.API.Model.Order;
using BigShoeCopmany.Model;

namespace BigShoeCompany.Service.Contracts
{
    public interface IOrderService
    {
        Task<bool> UploadOrderFileAsync(OrderBlobFileModel file);
        Task<List<OrderModel>> GetAllOrdersAsync();
    }
}