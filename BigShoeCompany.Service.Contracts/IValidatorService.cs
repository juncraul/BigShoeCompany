using BigShoeCopmany.Model;

namespace BigShoeCompany.Service.Contracts
{
    public interface IValidatorService
    {
        Task<bool> IsOrderFileValidAsync(Stream stream);
        bool IsValidOrders(List<OrderModel> orders);
    }
}
