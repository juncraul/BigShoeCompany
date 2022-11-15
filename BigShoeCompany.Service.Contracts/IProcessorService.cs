using BigShoeCopmany.Model;

namespace BigShoeCompany.Service.Contracts
{
    public interface IProcessorService
    {
        Task<List<OrderModel>> ProcessOrderFileAsync(Stream stream);
    }
}
