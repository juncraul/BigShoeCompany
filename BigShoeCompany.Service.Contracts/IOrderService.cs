using BigShoeCopmany.API.Model.Order;

namespace BigShoeCompany.Service.Contracts
{
    public interface IOrderService
    {
        Task<bool> UploadOrderFile(OrderBlobFileModel file);
    }
}