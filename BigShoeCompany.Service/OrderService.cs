using BigShoeCompany.DataAccess;
using BigShoeCompany.Service.Contracts;
using BigShoeCopmany.API.Model.Order;
using BigShoeCopmany.Model;

namespace BigShoeCompany.Service
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly IValidatorService _validatorService;
        private readonly IProcessorService _processorService;

        public OrderService(OrderRepository orderRepository,
            IValidatorService validatorService,
            IProcessorService processorService)
        {
            _orderRepository = orderRepository;
            _validatorService = validatorService;
            _processorService = processorService;
        }

        public async Task<bool> UploadOrderFileAsync(OrderBlobFileModel file)
        {
            //TODO: Upload the file to somewhere, maybe S3 bucket.

            using (var stream = file.File.OpenReadStream())
            {
                var isValidFile = await _validatorService.IsOrderFileValidAsync(stream);
                var orders = await _processorService.ProcessOrderFileAsync(stream);
                var isValidOrders = _validatorService.IsValidOrders(orders);
                foreach(var order in orders)
                {
                    await _orderRepository.AddNewOrderAsync(order);
                }
                return isValidOrders;
            }
        }

        public async Task<List<OrderModel>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }
    }
}