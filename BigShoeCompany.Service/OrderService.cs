using BigShoeCompany.Service.Contracts;
using BigShoeCopmany.API.Model.Order;

namespace BigShoeCompany.Service
{
    public class OrderService : IOrderService
    {
        private readonly IValidatorService _validatorService;
        private readonly IProcessorService _processorService;

        public OrderService(IValidatorService validatorService,
            IProcessorService processorService)
        {
            _validatorService = validatorService;
            _processorService = processorService;
        }

        public async Task<bool> UploadOrderFile(OrderBlobFileModel file)
        {
            //TODO: Upload the file to somewhere, maybe S3 bucket.

            using (var stream = file.File.OpenReadStream())
            {
                var isValidFile = await _validatorService.IsOrderFileValidAsync(stream);
                var orders = await _processorService.ProcessOrderFileAsync(stream);
                var isValidOrders = _validatorService.IsValidOrders(orders);
                return isValidOrders;
            }
        }
    }
}