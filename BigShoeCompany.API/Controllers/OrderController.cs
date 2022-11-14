using BigShoeCompany.FileManagement;
using BigShoeCompany.Service.Contracts;
using BigShoeCopmany.API.Model.Order;
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
            FileContentTypeModule.ValidateContentType(file.File.FileName, FileContentTypeModule.AcceptedFileTypeOrder);
            try
            {
                var result = await _orderService.UploadOrderFile(file);
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error uploading saas file", ex.Message);
                return new UnsupportedMediaTypeResult();
            }
        }
    }
}