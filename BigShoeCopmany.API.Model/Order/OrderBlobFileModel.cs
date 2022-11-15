using Microsoft.AspNetCore.Http;

namespace BigShoeCopmany.API.Model.Order
{
    public class OrderBlobFileModel
    {
        public IFormFile? File { get; set; }
    }
}