using BigShoeCompany.FileManagement;
using BigShoeCompany.Service.Contracts;
using BigShoeCopmany.API.Model.Order;
using BigShoeCopmany.Model;
using Microsoft.AspNetCore.Mvc;

namespace BigShoeCompany.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger,
            IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpPost]
        [Route("order/upload-order-file")]
        public async Task<IActionResult> UploadTemplateFile([FromForm] OrderBlobFileModel file)
        {
            if (file.File == null)
                return BadRequest("No file has been provided.");

            try
            {
                FileContentTypeModule.ValidateContentType(file.File.FileName, FileContentTypeModule.AcceptedFileTypeOrder);
                var result = await _orderService.UploadOrderFileAsync(file);
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error uploading order file", ex.Message);
                return BadRequest(ex.Message);
            }
        }



        [HttpGet]
        [Route("order/get-orders")]
        public async Task<List<OrderModel>> GetOrders()
        {
            return await _orderService.GetAllOrdersAsync();
        }
    }
}